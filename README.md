# String and LINQ Extension Methods

A comprehensive collection of useful extension methods for `string` and `IEnumerable<T>` in C#.

## Overview

This library provides two main sets of extension methods:
- **String Extensions**: 30+ methods for string manipulation, parsing, and analysis
- **LINQ Extensions**: Enhanced collection operations for working with IEnumerable<T>

---

## String Extension Methods

### Substring Extraction

#### Left
Extracts characters from the left side of a string.

**Overload 1: Extract by character count**
```csharp
string.Left(int numberOfCharacters)
```
- Returns the specified number of characters from the left
- Negative values extract from the right instead
- Returns entire string if count exceeds length

```csharp
"Hello World".Left(5)      // "Hello"
"Hello World".Left(-5)     // "World"
"Hello".Left(10)           // "Hello"
```

**Overload 2: Extract until substring**
```csharp
string.Left(string substring, bool includeSubstring = false)
```
- Returns all characters to the left of the first occurrence of substring
- Optional parameter to include the substring in result

```csharp
"Hello World".Left("World")        // "Hello "
"Hello World".Left("World", true)  // "Hello World"
```

#### Right
Extracts characters from the right side of a string.

**Overload 1: Extract by character count**
```csharp
string.Right(int numberOfCharacters)
```
- Returns the specified number of characters from the right
- Negative values extract from the left instead

```csharp
"Hello World".Right(5)     // "World"
"Hello World".Right(-5)    // "Hello"
```

**Overload 2: Extract from substring**
```csharp
string.Right(string substring, bool includeSubstring = false)
```
- Returns all characters to the right of the last occurrence of substring

```csharp
"Hello World".Right("Hello")        // " World"
"Hello World".Right("Hello", true)  // "Hello World"
```

#### Between
Extracts characters between two positions or substrings.

**Overload 1: Between indices**
```csharp
string.Between(int startIndex, int endIndex, bool includeBeginEndCharacters = false)
```
- Extracts characters between given indices
- Automatically swaps indices if start > end
- Optional parameter to include boundary characters

```csharp
"Hello World".Between(0, 4)         // "ell"
"Hello World".Between(0, 4, true)   // "Hello"
```

**Overload 2: Between substrings**
```csharp
string.Between(string startString, string endString, bool includeBeginEndStrings = false)
```
- Extracts text between first occurrence of startString and last occurrence of endString

```csharp
"<div>Content</div>".Between("<div>", "</div>")        // "Content"
"<div>Content</div>".Between("<div>", "</div>", true)  // "<div>Content</div>"
```

#### Middle
Extracts characters from the middle of a string.

```csharp
string.Middle(int numberOfCharactersFromLeft, int numberOfCharactersFromRight)
```
- Extracts specified number of characters from both sides of the string's midpoint

```csharp
"Hello World".Middle(2, 3)  // "loWor"
```

### Word Operations

#### Words
Gets a collection of words from the string.

```csharp
ICollection<string> Words(bool justUnique = false)
```
- Splits string by spaces
- Optional parameter to return only unique words

```csharp
"Hello World Hello".Words()          // ["Hello", "World", "Hello"]
"Hello World Hello".Words(true)      // ["Hello", "World"]
```

#### WordCount
Returns the number of words in the string.

```csharp
long WordCount(bool justUnique = false)
```

```csharp
"Hello World Hello".WordCount()      // 3
"Hello World Hello".WordCount(true)  // 2
```

#### WordFrequency
Returns a dictionary of words and their frequency.

```csharp
IDictionary<string, long> WordFrequency(int sortOrder = 0, bool caseInsensitive = false)
```
- `sortOrder`: < 0 for descending, > 0 for ascending, 0 for no sorting
- `caseInsensitive`: Whether to treat words case-insensitively

```csharp
"Hello world hello".WordFrequency(0, true)
// { "hello": 2, "world": 1 }
```

#### GetNthWord
Gets the nth word from the string.

```csharp
string GetNthWord(int nth, bool returnLastWordIfCountExceedsLength = true, bool justUniqueWords = false)
```

```csharp
"Hello World Test".GetNthWord(2)  // "World"
"Hello World Test".GetNthWord(10) // "Test" (returns last word)
```

### Sentence Operations

#### Sentences
Gets a collection of sentences from the string.

```csharp
ICollection<string> Sentences()
```
- Splits string by period (.)
- Note: May not handle URLs or abbreviations perfectly

```csharp
"Hello. World. Test.".Sentences()  // ["Hello", "World", "Test"]
```

#### SentenceCount
Returns the number of sentences.

```csharp
int SentenceCount()
```

### String Reversal

#### Reverse
Reverses the string or just the words.

```csharp
string Reverse(bool reverseWords = false)
```
- `reverseWords = false`: Reverses word order only
- `reverseWords = true`: Reverses entire string character by character

```csharp
"Hello World".Reverse()       // "World Hello"
"Hello World".Reverse(true)   // "dlroW olleH"
```

### Frequency and Search

#### Frequency
Gets the frequency of occurrence of a string or pattern.

```csharp
int Frequency(string stringToMatch, bool isRegEx = false, RegexOptions regexOptions = RegexOptions.None)
```

```csharp
"Hello Hello World".Frequency("Hello")      // 2
"abc123def456".Frequency(@"\d+", true)      // 2 (regex pattern)
```

#### NthIndexOf
Gets the index of the nth occurrence of a substring.

```csharp
int NthIndexOf(string substring, int nth)
```

```csharp
"Hello Hello Hello".NthIndexOf("Hello", 2)  // 6
```

### Truncation

#### Truncate
Truncates the string from left or right.

```csharp
string Truncate(int numberOfCharacters, string replacementString = "")
```
- Positive values truncate from right
- Negative values truncate from left

```csharp
"Hello World".Truncate(6, "...")      // "Hello..."
"Hello World".Truncate(-6, "...")     // "...World"
```

#### TruncateMiddle
Truncates characters from the middle of the string.

```csharp
string TruncateMiddle(int numberOfCharactersFromLeft, int numberOfCharactersFromRight, string replacementString = "")
```

```csharp
"Hello World".TruncateMiddle(2, 3, "...")  // "He...rld"
```

#### TruncateEnds
Truncates characters from both ends.

```csharp
string TruncateEnds(int numberOfCharactersFromLeft, int numberOfCharactersFromRight, string replacementString = "")
```

```csharp
"Hello World".TruncateEnds(2, 3, "")  // "llo Wo"
```

### Character Analysis

#### GetVowels / GetNumberOfVowels
Gets vowels or count of vowels in the string.

```csharp
IEnumerable<char> GetVowels(bool justUnique = false)
int GetNumberOfVowels(bool justUnique = false)
```

```csharp
"Hello".GetVowels()              // ['e', 'o']
"Hello".GetNumberOfVowels()      // 2
```

#### GetConsonants / GetNumberOfConsonants
Gets consonants or count of consonants in the string.

```csharp
IEnumerable<string> GetConsonants(bool justUnique = false)
int GetNumberOfConsonants(bool justUnique = false)
```

```csharp
"Hello".GetConsonants()          // ["H", "l", "l"]
"Hello".GetNumberOfConsonants()  // 3
```

#### GetNonVowels / GetNumberOfNonVowels
Gets all non-vowel characters (including spaces, numbers, etc.).

```csharp
IEnumerable<char> GetNonVowels(bool justUnique = false)
int GetNumberOfNonVowels(bool justUnique = false)
```

#### GetSpecialCharacters / GetNumberOfSpecialCharacters
Gets special characters (non-alphanumeric).

```csharp
IEnumerable<char> GetSpecialCharacters(bool justUnique = false)
int GetNumberOfSpecialCharacters(bool justUnique = false)
```

```csharp
"Hello, World!".GetSpecialCharacters()  // [',', ' ', '!']
```

#### GetDigits / GetNumberOfDigits
Gets digits from the string.

```csharp
IEnumerable<int> GetDigits(bool justUnique = false)
int GetNumberOfDigits(bool justUnique = false)
```

```csharp
"abc123def456".GetDigits()       // [1, 2, 3, 4, 5, 6]
"abc123def456".GetNumberOfDigits() // 6
```

### String Validation

#### IsPalindrome
Checks if the string is a palindrome.

```csharp
bool IsPalindrome()
```

```csharp
"racecar".IsPalindrome()  // true
"hello".IsPalindrome()    // false
```

### String Splitting

#### SplitUsingRegex
Splits a string using a regular expression pattern.

```csharp
IEnumerable<string> SplitUsingRegex(string regexPattern, RegexOptions regexOptions = RegexOptions.None)
```

```csharp
"a1b2c3".SplitUsingRegex(@"\d")  // ["a", "b", "c"]
```

### Pattern Extraction

#### GetUrls
Extracts URLs from the string.

```csharp
IEnumerable<string> GetUrls(string urlRegularExpression = "[default pattern]")
```

```csharp
"Visit https://example.com".GetUrls()  // ["https://example.com"]
```

#### GetPhoneNumbers
Extracts US phone numbers from the string.

```csharp
IEnumerable<string> GetPhoneNumbers(string phoneNumberRegularExpression = "[default pattern]")
```

```csharp
"Call 555-123-4567".GetPhoneNumbers()  // ["555-123-4567"]
```

#### GetSsns
Extracts US Social Security Numbers from the string.

```csharp
IEnumerable<string> GetSsns(string ssnRegularExpression = "[default pattern]")
```

```csharp
"SSN: 123-45-6789".GetSsns()  // ["123-45-6789"]
```

### String Replacement

#### ReplaceNth
Replaces the nth occurrence of a substring.

```csharp
string ReplaceNth(string stringToReplace, string replacementString, int nth)
```

```csharp
"Hello Hello Hello".ReplaceNth("Hello", "Hi", 2)  // "Hello Hi Hello"
```

#### ReplaceMultiple
Replaces multiple substrings at once.

```csharp
string ReplaceMultiple(IEnumerable<string> stringsToReplace, IEnumerable<string> replacementStrings)
```

```csharp
"Hello World".ReplaceMultiple(new[] {"Hello", "World"}, new[] {"Hi", "Earth"})
// "Hi Earth"
```

### HTML Operations

#### RemoveHtmlTags
Removes HTML tags from the string.

```csharp
string RemoveHtmlTags(string specificHtmlTagRegex = "", RegexOptions regexOptions = RegexOptions.None)
```

```csharp
"<div>Hello</div>".RemoveHtmlTags()  // "Hello"
```

#### ConvertBr2Newline
Converts HTML line break tags to newline characters.

```csharp
string ConvertBr2Newline()
```

```csharp
"Line1<br>Line2<br/>Line3".ConvertBr2Newline()
// "Line1\nLine2\nLine3"
```

---

## LINQ Extension Methods

### SkipLast
Skips the specified number of items from the end of a collection.

```csharp
IEnumerable<T> SkipLast<T>(int numberOfItemsToSkip)
```
- Positive values skip from the end
- Negative values skip from the beginning

```csharp
new[] {1, 2, 3, 4, 5}.SkipLast(2)   // [1, 2, 3]
new[] {1, 2, 3, 4, 5}.SkipLast(-2)  // [3, 4, 5]
```

### TakeLast
Takes the specified number of items from the end of a collection.

```csharp
IEnumerable<T> TakeLast<T>(int numberOfItemsToTake)
```
- Positive values take from the end
- Negative values take from the beginning

```csharp
new[] {1, 2, 3, 4, 5}.TakeLast(2)   // [4, 5]
new[] {1, 2, 3, 4, 5}.TakeLast(-2)  // [1, 2]
```

### TakeNs
Takes items at specified indices from the collection.

```csharp
IEnumerable<T> TakeNs<T>(params int[] indexOfItemsToTake)
```

```csharp
new[] {1, 2, 3, 4, 5}.TakeNs(0, 2, 4)  // [1, 3, 5]
```

### SkipNs
Skips items at specified indices from the collection.

```csharp
IEnumerable<T> SkipNs<T>(params int[] indexOfItemsToSkip)
```

```csharp
new[] {1, 2, 3, 4, 5}.SkipNs(1, 3)  // [1, 3, 5]
```

---

## Installation

1. Clone the repository
2. Add references to `StringExtensions.dll` and `LINQExtensions.dll` in your project
3. Add using statements:
   ```csharp
   using StringExtensions;
   using LINQExtensions;
   ```

## License

Copyright (c) 2014 Partha Sarathi

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues for bugs and feature requests.
