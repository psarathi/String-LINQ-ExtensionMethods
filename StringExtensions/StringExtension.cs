/*
 *  Copyright (c) 2014 Partha Sarathi
 *  Permission is hereby granted, free of charge, to any person obtaining a copy
 *  of this software and associated documentation files (the "Software"), to deal
 *  in the Software without restriction, including without limitation the rights
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *  copies of the Software, and to permit persons to whom the Software is
 *  furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Text.RegularExpressions;

namespace StringExtensions;

/// <summary>
/// Class containing the following C# string extensions
/// Left
/// Right
/// Between
/// Words
/// Word Count
/// Word Frequency
/// Sentences
/// Sentence Count
/// Reverse
/// Reverse Words
/// String Frequency
/// Middle
/// Truncate left, right, middle
/// nth word
/// nth index
/// Number of vowels/consonants
/// Split based on a regular expression
/// Digits
/// Find specific strings e.g. Urls, phone numbers etc.
/// ReplaceNths
/// --TODO--
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// The delegate that returns all the matches for a given string and pattern
    /// </summary>
    private static IEnumerable<Match> RegexMatches(string source, string pattern, RegexOptions options) =>
        Regex.Matches(source, pattern, options).Cast<Match>();

    /// <summary>
    /// Gets the specified number of characters from the left of the given string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="numberOfCharacters">Number of characters to be extracted from left</param>
    /// <returns>The string of extracted characters</returns>
    public static string? Left(this string? source, int numberOfCharacters)
    {
        if (source is null)
            return null;

        // Get characters from the right if number of characters is less than 0
        if (numberOfCharacters < 0)
            return Right(source, Math.Abs(numberOfCharacters));

        if (numberOfCharacters == 0)
            return string.Empty;

        return numberOfCharacters >= source.Length ? source : source[..numberOfCharacters];
    }

    /// <summary>
    /// Gets all the characters from the left of a given substring
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="subString">The substring to be matched</param>
    /// <param name="includeSubstring">Optional: Whether to include the substring in the result</param>
    /// <returns>The string of extracted characters</returns>
    public static string? Left(this string? source, string? subString, bool includeSubstring = false)
    {
        if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(subString))
            return null;

        var indexOfSubstring = source.IndexOf(subString, StringComparison.OrdinalIgnoreCase);

        return indexOfSubstring < 0
            ? string.Empty
            : includeSubstring
                ? Left(source, indexOfSubstring) + subString
                : Left(source, indexOfSubstring);
    }

    /// <summary>
    /// Gets the specified number of characters from the right of the given string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="numberOfCharacters">Number of characters to be extracted from right</param>
    /// <returns>The string of extracted characters</returns>
    public static string? Right(this string? source, int numberOfCharacters)
    {
        if (source is null)
            return null;

        // Get characters from the left if number of characters is less than 0
        if (numberOfCharacters < 0)
            return Left(source, Math.Abs(numberOfCharacters));

        if (numberOfCharacters == 0)
            return string.Empty;

        return numberOfCharacters >= source.Length ? source : source[^numberOfCharacters..];
    }

    /// <summary>
    /// Gets all the characters from the right of a given substring
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="subString">The substring to be matched</param>
    /// <param name="includeSubstring">Optional: Whether to include the substring in the result</param>
    /// <returns>The string of extracted characters</returns>
    public static string? Right(this string? source, string? subString, bool includeSubstring = false)
    {
        if (string.IsNullOrWhiteSpace(subString) || source is null)
            return null;

        var indexOfSubstring = source.LastIndexOf(subString, StringComparison.OrdinalIgnoreCase);

        return indexOfSubstring < 0
            ? string.Empty
            : includeSubstring
                ? subString + Right(source, indexOfSubstring + subString.Length)
                : Right(source, indexOfSubstring + subString.Length);
    }

    /// <summary>
    /// Gets all the characters between given indices
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="startIndex">Extraction start position</param>
    /// <param name="endIndex">Extraction end position</param>
    /// <param name="includeBeginEndCharacters">Optional: Whether to include the characters at the start and end points</param>
    /// <returns>The string of extracted characters</returns>
    public static string? Between(this string? source, int startIndex, int endIndex, bool includeBeginEndCharacters = false)
    {
        if (source is null)
            return null;

        // Use the absolute values of the indices
        startIndex = Math.Abs(startIndex);
        endIndex = Math.Abs(endIndex);

        if (startIndex > source.Length)
            return string.Empty;

        // Swap if start index is greater than end index
        if (startIndex > endIndex)
            (startIndex, endIndex) = (endIndex, startIndex);

        // Set endIndex to the length of the string if it is greater than that
        if (endIndex > source.Length)
            endIndex = source.Length - 1;

        return includeBeginEndCharacters
            ? source[startIndex..(endIndex + 1)]
            : source[(startIndex + 1)..endIndex];
    }

    /// <summary>
    /// Gets the string in between two given strings
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="startString">The string to start the search</param>
    /// <param name="endString">The string to end the search</param>
    /// <param name="includeBeginEndStrings">Optional: Whether the start and end strings will be included in the result</param>
    /// <returns>The string in between the given strings</returns>
    public static string? Between(this string? source, string? startString, string? endString, bool includeBeginEndStrings = false)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(startString) || string.IsNullOrEmpty(endString))
            return null;

        var indexOfStart = source.IndexOf(startString, StringComparison.Ordinal);
        var lastIndexOfEnd = source.LastIndexOf(endString, StringComparison.Ordinal);

        // If either the start or end string was not found then return null
        if (indexOfStart == -1 || lastIndexOfEnd == -1 || indexOfStart >= lastIndexOfEnd)
            return null;

        var result = source.Between(indexOfStart + startString.Length - 1, lastIndexOfEnd);

        return includeBeginEndStrings ? startString + result + endString : result;
    }

    /// <summary>
    /// Gets the desired number of characters from the left and right starting from the middle of the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="numberOfCharactersFromLeft">Number of characters to be extracted to left of the middle</param>
    /// <param name="numberOfCharactersFromRight">Number of characters to be extracted to right of the middle</param>
    /// <returns>The result string</returns>
    public static string? Middle(this string? source, int numberOfCharactersFromLeft, int numberOfCharactersFromRight)
    {
        if (string.IsNullOrEmpty(source))
            return null;

        numberOfCharactersFromLeft = Math.Abs(numberOfCharactersFromLeft);
        numberOfCharactersFromRight = Math.Abs(numberOfCharactersFromRight);

        var middleIndexOfString = (int)Math.Round(source.Length / 2m);
        var distanceFromEnd = source.Length - middleIndexOfString;
        var leftString = numberOfCharactersFromLeft > middleIndexOfString
            ? source.Left(middleIndexOfString)
            : source[..middleIndexOfString].Right(numberOfCharactersFromLeft);
        var rightString = numberOfCharactersFromRight >= distanceFromEnd
            ? source[middleIndexOfString..]
            : source.Substring(middleIndexOfString, numberOfCharactersFromRight);

        return leftString + rightString;
    }

    /// <summary>
    /// Gets the list of words in the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="justUnique">Optional: Whether a unique list is required</param>
    /// <returns>A collection of words</returns>
    public static ICollection<string>? Words(this string? source, bool justUnique = false)
    {
        if (string.IsNullOrEmpty(source))
            return null;

        var words = source.Split([' '], StringSplitOptions.RemoveEmptyEntries);
        return justUnique ? words.Distinct().ToArray() : words;
    }

    /// <summary>
    /// Gets the number of words in the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="justUnique">Optional: Whether a unique list is required</param>
    /// <returns>The word count</returns>
    public static long WordCount(this string? source, bool justUnique = false) =>
        string.IsNullOrEmpty(source) ? 0 : source.Words(justUnique)?.Count ?? 0;

    /// <summary>
    /// Gets the frequency of the words in a string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="sortOrder">Optional: The order in which the dictionary will be sorted based on frequency &lt; 0 Descending, &gt; 0 Ascending, 0 No sorting order</param>
    /// <param name="caseInsensitive">Optional: Whether a case insensitive comparison is required</param>
    /// <returns>A dictionary of word and its frequency</returns>
    public static IDictionary<string, long>? WordFrequency(this string? source, int sortOrder = 0, bool caseInsensitive = false)
    {
        if (string.IsNullOrEmpty(source))
            return null;

        var wordFrequency = source.Words()?
            .GroupBy(w => caseInsensitive ? w.ToLower() : w)
            .Select(kv => new KeyValuePair<string, long>(kv.Key, kv.LongCount()));

        if (wordFrequency is null)
            return null;

        return sortOrder switch
        {
            0 => wordFrequency.ToDictionary(k => k.Key, v => v.Value),
            < 0 => wordFrequency.OrderByDescending(f => f.Value).ToDictionary(k => k.Key, v => v.Value),
            > 0 => wordFrequency.OrderBy(f => f.Value).ToDictionary(k => k.Key, v => v.Value)
        };
    }

    /// <summary>
    /// Gets the nth word in a string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="nth">The integer n</param>
    /// <param name="returnLastWordIfCountExceedsLength">Optional: Whether the last word will be returned if the value of n exceeds the string length</param>
    /// <param name="justUniqueWords">Optional: Whether only unique words need to be considered</param>
    /// <returns>The nth word</returns>
    public static string? GetNthWord(this string? source, int nth, bool returnLastWordIfCountExceedsLength = true, bool justUniqueWords = false)
    {
        if (string.IsNullOrEmpty(source) || nth == 0)
            return null;

        nth = Math.Abs(nth);
        var words = source.Words(justUniqueWords);

        if (words is null || words.Count == 0)
            return null;

        if (nth > words.Count)
            return returnLastWordIfCountExceedsLength ? words.ElementAt(words.Count - 1) : null;

        return words.ElementAt(nth - 1);
    }

    /// <summary>
    /// Gets the list of sentences in the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <returns>A collection of sentences</returns>
    public static ICollection<string>? Sentences(this string? source)
    {
        // todo: improve the algorithm to get sentences. Right now it'll treat every '.' as a sentence delimiter, which may not be true if there is e.g. a url in the string
        return string.IsNullOrEmpty(source)
            ? null
            : source.Split(['.'], StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToArray();
    }

    /// <summary>
    /// Gets the count of sentences in the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <returns>The count of sentences</returns>
    public static int SentenceCount(this string? source) =>
        string.IsNullOrEmpty(source) ? 0 : source.Sentences()?.Count ?? 0;

    /// <summary>
    /// Gets the reversed string or just the words in the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="reverseWords">Optional: Whether the words need to be reversed as well</param>
    /// <returns>The reversed string</returns>
    public static string? Reverse(this string? source, bool reverseWords = false)
    {
        if (string.IsNullOrEmpty(source))
            return null;

        return reverseWords
            ? string.Join("", source.Reverse())
            : string.Join(" ", source.Split(' ').Reverse());
    }

    /// <summary>
    /// Gets the frequency of occurrence of a given string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="stringToMatch">The string or pattern whose frequency is required</param>
    /// <param name="isRegEx">Optional: Whether the string is a regular expression pattern</param>
    /// <param name="regexOptions">Optional: The regular expression options</param>
    /// <returns>The frequency of occurrence of the given string or regular expression</returns>
    public static int Frequency(this string? source, string? stringToMatch, bool isRegEx = false, RegexOptions regexOptions = RegexOptions.None)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(stringToMatch))
            return 0;

        return isRegEx
            ? RegexMatches(source, stringToMatch, regexOptions).Count()
            : source.Split([stringToMatch], StringSplitOptions.None).Length - 1;
    }

    /// <summary>
    /// Gets the truncated version of the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="numberOfCharacters">Number of characters to truncate, negative value will truncate from left</param>
    /// <param name="replacementString">Optional: The optional string to replace the truncated characters e.g. ...</param>
    /// <returns>The truncated string</returns>
    public static string? Truncate(this string? source, int numberOfCharacters, string replacementString = "")
    {
        if (string.IsNullOrEmpty(source))
            return null;

        if (numberOfCharacters == 0)
            return source;

        // If the number of characters to truncate is less than 0 then truncate from left
        var stringToBeTruncated = numberOfCharacters < 0
            ? source.Left(Math.Abs(numberOfCharacters))
            : source.Right(numberOfCharacters);

        return string.IsNullOrEmpty(stringToBeTruncated)
            ? source
            : source.Replace(stringToBeTruncated, replacementString);
    }

    /// <summary>
    /// Gets the truncated version of the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="numberOfCharactersFromLeft">Number of characters to truncate from left</param>
    /// <param name="numberOfCharactersFromRight">Number of characters to truncate from right</param>
    /// <param name="replacementString">Optional: The optional string to replace the truncated characters e.g. ...</param>
    /// <returns>The truncated string</returns>
    public static string? TruncateMiddle(this string? source, int numberOfCharactersFromLeft, int numberOfCharactersFromRight, string replacementString = "")
    {
        if (string.IsNullOrEmpty(source))
            return null;

        var stringToBeTruncated = source.Middle(numberOfCharactersFromLeft, numberOfCharactersFromRight);

        return string.IsNullOrEmpty(stringToBeTruncated)
            ? source
            : source.Replace(stringToBeTruncated, replacementString);
    }

    /// <summary>
    /// Truncates the ends of a string and replaces with the replacement string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="numberOfCharactersFromLeft">Number of characters to be truncated from the left, negative value will truncate from right</param>
    /// <param name="numberOfCharactersFromRight">Number of characters to be truncated from the right, negative value will truncate from left</param>
    /// <param name="replacementString">The replacement string to be used</param>
    /// <returns>The string truncated at both ends</returns>
    public static string? TruncateEnds(this string? source, int numberOfCharactersFromLeft, int numberOfCharactersFromRight, string replacementString = "") =>
        source.Truncate(numberOfCharactersFromRight, replacementString)
            ?.Truncate(-1 * numberOfCharactersFromLeft, replacementString);

    /// <summary>
    /// Gets the index of nth match of a substring
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="substring">The string to be searched</param>
    /// <param name="nth">The nth match</param>
    /// <returns>The index of the nth match</returns>
    public static int NthIndexOf(this string? source, string? substring, int nth)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(substring) || nth <= 0)
            return -1;

        var matches = RegexMatches(source, substring, RegexOptions.None).ToArray();

        return nth > matches.Length
            ? matches[^1].Index
            : matches[nth - 1].Index;
    }

    /// <summary>
    /// Gets all the vowels in a string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="justUnique">Optional: Whether only unique vowels need to be considered</param>
    /// <returns>All the vowels in the string</returns>
    public static IEnumerable<char>? GetVowels(this string? source, bool justUnique = false)
    {
        if (string.IsNullOrEmpty(source))
            return null;

        var vowels = RegexMatches(source, @"[aeiouAEIOU]", RegexOptions.None)
            .Select(m => m.Value[0])
            .ToArray();

        return justUnique ? vowels.Distinct() : vowels;
    }

    /// <summary>
    /// Gets the count of vowels in a string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="justUnique">Optional: Whether only unique vowels need to be considered</param>
    /// <returns>The count of vowels in the string</returns>
    public static int GetNumberOfVowels(this string? source, bool justUnique = false) =>
        string.IsNullOrEmpty(source) ? 0 : source.GetVowels(justUnique)?.Count() ?? 0;

    /// <summary>
    /// Gets all the consonants in a string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="justUnique">Optional: Whether only unique consonants need to be considered</param>
    /// <returns>All the consonants in the string</returns>
    public static IEnumerable<string>? GetConsonants(this string? source, bool justUnique = false)
    {
        if (string.IsNullOrEmpty(source))
            return null;

        var consonants = RegexMatches(source, @"[a-zA-Z]", RegexOptions.None)
            .Select(m => m.Value)
            .ToArray();

        return consonants.Length > 0
            ? justUnique ? consonants.Distinct() : consonants
            : null;
    }

    /// <summary>
    /// Gets the count of consonants in a string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="justUnique">Optional: Whether only unique consonants need to be considered</param>
    /// <returns>The count of consonants in the string</returns>
    public static int GetNumberOfConsonants(this string? source, bool justUnique = false) =>
        string.IsNullOrEmpty(source) ? 0 : source.GetConsonants(justUnique)?.Count() ?? 0;

    /// <summary>
    /// Gets all the non-vowels in a string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="justUnique">Optional: Whether only unique non-vowels need to be considered</param>
    /// <returns>All the non-vowels in the string</returns>
    public static IEnumerable<char>? GetNonVowels(this string? source, bool justUnique = false)
    {
        if (string.IsNullOrEmpty(source))
            return null;

        var consonants = RegexMatches(source, @"[^aeiouAEIOU]", RegexOptions.None)
            .Select(m => m.Value[0])
            .ToArray();

        return justUnique ? consonants.Distinct() : consonants;
    }

    /// <summary>
    /// Gets the count of non-vowels in a string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="justUnique">Optional: Whether only unique non-vowels need to be considered</param>
    /// <returns>The count of non-vowels in the string</returns>
    public static int GetNumberOfNonVowels(this string? source, bool justUnique = false) =>
        string.IsNullOrEmpty(source) ? 0 : source.GetNonVowels(justUnique)?.Count() ?? 0;

    /// <summary>
    /// Gets the special characters in the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="justUnique">Optional: Whether only unique special characters need to be considered</param>
    /// <returns>The list of special characters in the string</returns>
    public static IEnumerable<char>? GetSpecialCharacters(this string? source, bool justUnique = false)
    {
        if (string.IsNullOrEmpty(source))
            return null;

        var specialCharacters = RegexMatches(source, @"[^a-zA-Z0-9]", RegexOptions.None)
            .Select(m => m.Value[0])
            .ToArray();

        return justUnique ? specialCharacters.Distinct() : specialCharacters;
    }

    /// <summary>
    /// Gets the count of special characters in the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="justUnique">Optional: Whether only unique special characters need to be considered</param>
    /// <returns>The count of special characters in the string</returns>
    public static int GetNumberOfSpecialCharacters(this string? source, bool justUnique = false) =>
        string.IsNullOrEmpty(source) ? 0 : source.GetSpecialCharacters(justUnique)?.Count() ?? 0;

    /// <summary>
    /// Tells whether the string is a palindrome
    /// </summary>
    /// <param name="source">The source string</param>
    /// <returns>True, if string is a palindrome, false otherwise</returns>
    public static bool IsPalindrome(this string? source) =>
        !string.IsNullOrEmpty(source) && source.Equals(source.Reverse(), StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Splits a string based on a regular expression
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="regexPattern">The regular expression used for splitting</param>
    /// <param name="regexOptions">Optional: The regular expression options</param>
    /// <returns>An array of strings based on the split</returns>
    public static IEnumerable<string>? SplitUsingRegex(this string? source, string? regexPattern, RegexOptions regexOptions = RegexOptions.None)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(regexPattern))
            return null;

        var matchValues = RegexMatches(source, regexPattern, regexOptions)
            .Select(m => m.Value)
            .ToArray();

        return matchValues.Length > 0
            ? source.Split(matchValues, StringSplitOptions.None)
            : null;
    }

    /// <summary>
    /// Gets the list of all the URLs in the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="urlRegularExpression">Optional: The regular expression for URL</param>
    /// <returns>The list of URLs in the string</returns>
    public static IEnumerable<string>? GetUrls(this string? source,
        string urlRegularExpression = @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)")
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(urlRegularExpression))
            return null;

        var urls = RegexMatches(source, urlRegularExpression, RegexOptions.None)
            .Select(m => m.Value)
            .ToArray();

        return urls.Length > 0 ? urls.Distinct() : null;
    }

    /// <summary>
    /// Gets the digits in the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="justUnique">Optional: Whether only unique digits need to be considered</param>
    /// <returns>The list of digits in the string</returns>
    public static IEnumerable<int>? GetDigits(this string? source, bool justUnique = false)
    {
        if (string.IsNullOrEmpty(source))
            return null;

        var digits = RegexMatches(source, @"\d", RegexOptions.None)
            .Select(m => int.Parse(m.Value))
            .ToArray();

        return digits.Length > 0
            ? justUnique ? digits.Distinct() : digits
            : null;
    }

    /// <summary>
    /// Gets the number of digits in the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="justUnique">Optional: Whether only unique digits need to be considered</param>
    /// <returns>The count of digits in the string</returns>
    public static int GetNumberOfDigits(this string? source, bool justUnique = false) =>
        string.IsNullOrEmpty(source) ? 0 : source.GetDigits(justUnique)?.Count() ?? 0;

    /// <summary>
    /// Gets the list of all the US phone numbers in the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="phoneNumberRegularExpression">Optional: The regular expression for US phone number</param>
    /// <returns>The list of US phone numbers in the string</returns>
    public static IEnumerable<string>? GetPhoneNumbers(this string? source,
        string phoneNumberRegularExpression = @"(?:\+?1[-. ]?)?\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})")
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(phoneNumberRegularExpression))
            return null;

        var phoneNumbers = RegexMatches(source, phoneNumberRegularExpression, RegexOptions.None)
            .Select(m => m.Value)
            .ToArray();

        return phoneNumbers.Length > 0 ? phoneNumbers.Distinct() : null;
    }

    /// <summary>
    /// Gets the list of all the US social security numbers in the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="ssnRegularExpression">Optional: The regular expression for US social security number</param>
    /// <returns>The list of US social security numbers in the string</returns>
    public static IEnumerable<string>? GetSsns(this string? source, string ssnRegularExpression = @"\d{3}\-?\d{2}\-?\d{4}$")
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(ssnRegularExpression))
            return null;

        var ssns = RegexMatches(source, ssnRegularExpression, RegexOptions.None)
            .Select(m => m.Value)
            .ToArray();

        return ssns.Length > 0 ? ssns.Distinct() : null;
    }

    /// <summary>
    /// Gets the string after replacing the nth match with the given string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="stringToReplace">The string that needs to be replaced</param>
    /// <param name="replacementString">The replacement string</param>
    /// <param name="nth">The nth occurrence to replace</param>
    /// <returns>The result with the replaced string</returns>
    public static string? ReplaceNth(this string? source, string? stringToReplace, string? replacementString, int nth)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(stringToReplace) || nth == 0)
            return source;

        nth = Math.Abs(nth);
        var matches = RegexMatches(source, stringToReplace, RegexOptions.None).ToArray();

        if (nth > matches.Length)
            nth = matches.Length;

        return source.Remove(matches[nth - 1].Index, stringToReplace.Length)
            .Insert(matches[nth - 1].Index, replacementString ?? string.Empty);
    }

    /// <summary>
    /// Replaces multiple substrings in the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="stringsToReplace">The list of strings to replace</param>
    /// <param name="replacementStrings">The list of corresponding replacement strings</param>
    /// <returns>The result string with all the replacements</returns>
    public static string? ReplaceMultiple(this string? source, IEnumerable<string>? stringsToReplace, IEnumerable<string>? replacementStrings)
    {
        if (string.IsNullOrEmpty(source) || stringsToReplace is null || replacementStrings is null)
            return source;

        var result = source;
        foreach (var (stringToReplace, replacement) in stringsToReplace.Zip(replacementStrings))
        {
            result = result.Replace(stringToReplace, replacement);
        }

        return result;
    }

    /// <summary>
    /// Removes the HTML tags from the string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="specificHtmlTagRegex">Optional: Regular expression pattern for any specific tags</param>
    /// <param name="regexOptions">Optional: Regular expression options</param>
    /// <returns>The string without the HTML tags</returns>
    public static string? RemoveHtmlTags(this string? source, string specificHtmlTagRegex = "", RegexOptions regexOptions = RegexOptions.None)
    {
        if (string.IsNullOrEmpty(source))
            return null;

        return string.IsNullOrEmpty(specificHtmlTagRegex)
            ? Regex.Replace(source, @"<.*?>", string.Empty, regexOptions)
            : Regex.Replace(source, specificHtmlTagRegex, string.Empty, regexOptions);
    }

    /// <summary>
    /// Replaces the HTML line break tags with new line characters
    /// </summary>
    /// <param name="source">The source string</param>
    /// <returns>The string with line break tags replaced by new lines</returns>
    public static string? ConvertBr2Newline(this string? source) =>
        string.IsNullOrEmpty(source) ? null : Regex.Replace(source, @"<br.*?>", Environment.NewLine);
}
