# coding-challenge_ironsoft

## Algorithm

## Old Phone Keypad Rules (C# Coding Challenge)

* Digits **2–9** map to letters:

  * **2 → ABC**, **3 → DEF**, ..., **9 → WXYZ**.
* **0** maps to a **space character**.
* Pressing the same key multiple times cycles through its letters:

  * `2 → A`, `22 → B`, `222 → C`, `2222 → A` (wrap-around).
* A **space in the input** represents a pause required when typing consecutive letters from the same key:

  * Example: `222 2 22 → C A B`.
* `*` acts as **backspace** (removes the last produced character).
* `#` is **send**, marking the **end of input**.
* **Spaces** in the input are **separators**, not real characters.

## Parsing Strategy

1. Iterate over input characters.
2. Maintain:

   * The **current key** being pressed.
   * The **count** of how many times it has been pressed.
3. When encountering:

   * A **different digit**,
   * A **space `' '`**,
   * A **`*`**, or
   * A **`#`**,
     commit the current keypress group:
   * Convert the stored key + count into the corresponding character using mapping + cycling.
   * Reset key/count afterwards.
4. On `*`:

   * After committing the pending keypress group, **delete the last output character** (if one exists).
5. On `#`:

   * Commit any pending keypress group.
   * **Finish** processing.
