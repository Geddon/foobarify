using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Foobarify.Business.Extensions
{
    public static class StringExtensions
    {
        public static string FooBarify(this string stringToFoobarify, string customPrefix = "", string customSuffix = "")
        {
            if (String.IsNullOrWhiteSpace(stringToFoobarify))
            {
                return string.Empty;
            }

            //TODO this method should be replaced with word by word replacement to preserve case in all char positions

            var prefix = "FOO";
            var suffix = @"BAR";

            if (!String.IsNullOrWhiteSpace(customPrefix))
            {
                prefix = customPrefix + prefix;
            }
            if (!String.IsNullOrWhiteSpace(customSuffix))
            {
                suffix = suffix + customSuffix;
            }

            var tokens = stringToFoobarify.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ").Split(' ')
                .Where(x => !String.IsNullOrWhiteSpace(x) && (x.Length > 1 || x.All(Char.IsLetter)))
                .Select(x => x.Trim());

            var groupedTokens = tokens.GroupBy(x => x).OrderByDescending(x => x.Count());

            var mostUsedWord = groupedTokens.First().Key;
            var mostUsedWordFirstLetterUppercase = mostUsedWord.First().ToString().ToUpper() + mostUsedWord.Substring(1);
            
            return stringToFoobarify
                .ReplaceWholeWords(mostUsedWord, prefix + mostUsedWord + suffix)
                .ReplaceWholeWords(mostUsedWordFirstLetterUppercase, prefix + mostUsedWordFirstLetterUppercase + suffix);
        }

        public static string ReplaceWholeWords(this string str, string oldValue, string newValue)
        {
            var pattern = @"\b" + oldValue + @"\b";

            return Regex.Replace(str, pattern, newValue);
        }
    }
}