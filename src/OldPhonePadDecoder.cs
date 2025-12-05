using System;
using System.Collections.Generic;
using System.Text;

public static class OldPhonePadDecoder
{
    private static readonly IReadOnlyDictionary<char, char[]> KeyToLetters =
        new Dictionary<char, char[]>
        {
            { '1', new[] { '&', '\'', '(' } }, // for completeness
            { '2', new[] { 'A', 'B', 'C' } },
            { '3', new[] { 'D', 'E', 'F' } },
            { '4', new[] { 'G', 'H', 'I' } },
            { '5', new[] { 'J', 'K', 'L' } },
            { '6', new[] { 'M', 'N', 'O' } },
            { '7', new[] { 'P', 'Q', 'R', 'S' } },
            { '8', new[] { 'T', 'U', 'V' } },
            { '9', new[] { 'W', 'X', 'Y', 'Z' } },
            { '0', new[] { ' ' } }
        };

    /// <summary>
    /// Decodes old-phone keypad input into text.
    /// '*' is backspace, spaces separate groups, '#' terminates input.
    /// </summary>
    public static string OldPhonePad(string input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));

        var output = new StringBuilder();
        char? pendingKey = null;
        int pendingPressCount = 0;

        foreach (char ch in input)
        {
            if (ch == '#')
            {
                // commit and exit
                CommitPendingKey(ref pendingKey, ref pendingPressCount, output);
                break;
            }

            if (ch == '*')
            {
                CommitPendingKey(ref pendingKey, ref pendingPressCount, output);
                if (output.Length > 0) output.Remove(output.Length - 1, 1);
                continue;
            }

            if (ch == ' ')
            {
                CommitPendingKey(ref pendingKey, ref pendingPressCount, output);
                continue;
            }

            if (char.IsDigit(ch))
            {
                if (pendingKey == null || pendingKey.Value == ch)
                {
                    pendingKey = ch;
                    pendingPressCount++;
                }
                else
                {
                    CommitPendingKey(ref pendingKey, ref pendingPressCount, output);
                    pendingKey = ch;
                    pendingPressCount = 1;
                }

                continue;
            }

            // ignore other characters safely
        }

        // Defensive: if input had no '#', commit any pending key.
        CommitPendingKey(ref pendingKey, ref pendingPressCount, output);

        return output.ToString();
    }

    private static void CommitPendingKey(ref char? pendingKey, ref int pendingPressCount, StringBuilder output)
    {
        if (pendingKey == null || pendingPressCount == 0) return;

        if (!KeyToLetters.TryGetValue(pendingKey.Value, out var letters) || letters.Length == 0)
        {
            pendingKey = null;
            pendingPressCount = 0;
            return;
        }

        int index = (pendingPressCount - 1) % letters.Length;
        output.Append(letters[index]);

        pendingKey = null;
        pendingPressCount = 0;
    }
}
