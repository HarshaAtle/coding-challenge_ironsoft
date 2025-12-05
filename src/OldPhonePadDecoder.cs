using System;
using System.Collections.Generic;
using System.Text;

public static class OldPhonePadDecoder
{
    // Key mapping based on classic phone keypad
    private static readonly IReadOnlyDictionary<char, string> KeyMap =
        new Dictionary<char, string>
        {
            { '1', "&'(" },        // Not used in examples, but mapped for completeness
            { '2', "ABC" },
            { '3', "DEF" },
            { '4', "GHI" },
            { '5', "JKL" },
            { '6', "MNO" },
            { '7', "PQRS" },
            { '8', "TUV" },
            { '9', "WXYZ" },
            { '0', " " }           // Zero produces a space character
        };

    /// <summary>
    /// Converts an old-phone keypad input sequence into text.
    /// Assumptions:
    /// - Input always ends with '#' (send).
    /// - '*' acts as backspace on the already produced output.
    /// - Spaces in the input separate keypress groups; they are not characters.
    /// </summary>
    /// <param name="input">Keypad input string ending with '#'.</param>
    /// <returns>Decoded text output.</returns>
    public static string OldPhonePad(string input)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input));

        var output = new StringBuilder();
        char? currentKey = null;
        int pressCount = 0;

        void CommitCurrentKey()
        {
            if (currentKey is null || pressCount == 0)
                return;

            if (!KeyMap.TryGetValue(currentKey.Value, out var letters) || letters.Length == 0)
            {
                // Unknown key or no letters mapped: ignore this group.
                currentKey = null;
                pressCount = 0;
                return;
            }

            // Cycle through letters when pressCount exceeds the number of letters.
            int index = (pressCount - 1) % letters.Length;
            output.Append(letters[index]);

            currentKey = null;
            pressCount = 0;
        }

        foreach (char rawChar in input)
        {
            char ch = rawChar;

            if (ch == '#')
            {
                // Send: commit and finish
                CommitCurrentKey();
                break;
            }

            if (ch == '*')
            {
                // Backspace: commit previous key (if any), then remove last output char
                CommitCurrentKey();
                if (output.Length > 0)
                {
                    output.Remove(output.Length - 1, 1);
                }
                continue;
            }

            if (ch == ' ')
            {
                // Pause between characters
                CommitCurrentKey();
                continue;
            }

            if (char.IsDigit(ch))
            {
                if (currentKey is null || ch == currentKey.Value)
                {
                    // Same key pressed again
                    currentKey = ch;
                    pressCount++;
                }
                else
                {
                    // New key: commit previous and start new group
                    CommitCurrentKey();
                    currentKey = ch;
                    pressCount = 1;
                }

                continue;
            }

            // Any other characters are ignored safely.
        }

        return output.ToString();
    }
}
