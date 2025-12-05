# OldPhonePad Decoder

A C# implementation of an old-phone keypad decoder that converts classic phone keypad input sequences into decoded text.

## Overview

This project implements a solution to decode old-phone keypad input, as found on classic mobile phones (T9/multi-tap systems). The decoder processes sequences of digit presses, special characters, and pauses to produce readable text output.

## Features

- **Multi-press letter cycling**: Pressing the same key multiple times cycles through its mapped letters with automatic wrap-around
- **Space character support**: Key `0` produces a space character
- **Backspace functionality**: Key `*` acts as backspace to delete the last character
- **Pause-separated groups**: Spaces in input separate keypress groups, enabling consecutive letters from the same key
- **Production-grade code**: Full null validation, defensive coding, and comprehensive test coverage

## Key Mapping

Classic phone keypad letter mappings:

| Key | Letters |
|-----|---------|
| 0   | (space) |
| 1   | &'(     |
| 2   | ABC     |
| 3   | DEF     |
| 4   | GHI     |
| 5   | JKL     |
| 6   | MNO     |
| 7   | PQRS    |
| 8   | TUV     |
| 9   | WXYZ    |

## Usage

### Basic Example

```csharp
using System;

public static void Main()
{
    // Decode "HELLO"
    string result = OldPhonePadDecoder.OldPhonePad("4433555 555666#");
    Console.WriteLine(result); // Output: HELLO
}
```

### Input Format

- **Digits `0–9`**: Keypress sequences to decode
- **Space `' '`**: Pause/separator between keypress groups
- **`*`**: Backspace (deletes the last character)
- **`#`**: Send/finish (marks end of input, required)

### Examples

| Input | Output | Notes |
|-------|--------|-------|
| `33#` | `E` | Single letter (2 presses of key 3) |
| `227*#` | `B` | Type A, backspace, type B |
| `4433555 555666#` | `HELLO` | Multi-character with pauses |
| `8 88777444666*664#` | `TURING` | Complex sequence with backspace |
| `0#` | ` ` | Space character |
| `7777#` | `S` | Wrap-around on key 7 (PQRS) |

## Project Structure

```
.
├── src/
│   └── OldPhonePadDecoder.cs       # Main implementation
├── tests/
│   └── OldPhonePadDecoder.Tests.cs # xUnit test suite (8 test cases)
├── OldPhonePadDecoder.csproj       # .NET 10.0 project file
├── AI_PROMPT.md                    # AI usage documentation
└── README.md                        # This file
```

## Building and Testing

### Prerequisites

- .NET 10.0 SDK or later
- C# compiler support

### Build

```bash
dotnet build
```

### Run Tests

```bash
dotnet test
```

**Test Results**: All 8 unit tests pass, covering:
- Single-letter decoding
- Multi-press cycling with wrap-around
- Backspace functionality
- Pause-separated character groups
- Space character output
- Complex sequences with multiple operations

## Algorithm

### Parsing Strategy

1. **Iterate** over input characters maintaining current key and press count
2. **Commit** the current keypress group when encountering:
   - A different digit
   - A space (pause)
   - `*` (backspace)
   - `#` (send/finish)
3. **Convert** key + count to character using modulo arithmetic for wrap-around: `(pressCount - 1) % lettersLength`
4. **Handle special cases**:
   - `*`: Delete last output character if one exists
   - `#`: Finish processing

### Implementation Details

- **KeyMap**: Read-only dictionary for immutable, efficient character mapping
- **StringBuilder**: Used for efficient character accumulation
- **Local function**: `CommitCurrentKey()` encapsulates key-to-character conversion logic
- **Null safety**: Validates input and handles edge cases defensively
- **Wrap-around logic**: Modulo operator handles key cycling automatically

## AI Usage Declaration

This project complies fully with Iron Software's challenge guidelines. **All problem-solving, algorithm design, and implementation were completed independently.** 

AI tools were used **exclusively for post-implementation polishing**:
- Code review and structural suggestions
- Documentation and naming clarity
- Test coverage recommendations
- Production-grade engineering practices

See `AI_PROMPT.md` for the exact prompt and scope of AI assistance.

**No AI-generated logic or algorithms are included in the final implementation.**

## License

This is a coding challenge submission for Iron Software.
