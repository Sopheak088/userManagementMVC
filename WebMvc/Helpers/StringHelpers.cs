using iThinking.Common;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebMvc.Helpers
{
    public static class StringHelpers
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        private static readonly HashSet<char> DefaultNonWordCharacters = new HashSet<char> { ',', '.', ':', ';' };

        public static string CropWholeWords(string valueHtml, int length, HashSet<char> nonWordCharacters = null)
        {
            string value = HtmlToString(valueHtml);
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (length < 0)
            {
                throw new ArgumentException("Negative values not allowed.", "length");
            }

            if (nonWordCharacters == null)
            {
                nonWordCharacters = DefaultNonWordCharacters;
            }

            if (length >= value.Length)
            {
                return value;
            }
            int end = length;

            for (int i = end; i > 0; i--)
            {
                if (value[i].IsWhitespace())
                {
                    break;
                }

                if (nonWordCharacters.Contains(value[i])
                    && (value.Length == i + 1 || value[i + 1] == ' '))
                {
                    //Removing a character that isn't whitespace but not part
                    //of the word either (ie ".") given that the character is
                    //followed by whitespace or the end of the string makes it
                    //possible to include the word, so we do that.
                    break;
                }
                end--;
            }

            if (end == 0)
            {
                //If the first word is longer than the length we favor
                //returning it as cropped over returning nothing at all.
                end = length;
            }

            string result = value.Substring(0, end);
            if (end < value.Length)
                result += "...";

            return result;
        }

        public static string HtmlToString(string valueHtml)
        {
            if (string.IsNullOrWhiteSpace(valueHtml))
                valueHtml = "";
            string value = HtmlToText.ConvertHtml(valueHtml);

            return value;
        }

        private static bool IsWhitespace(this char character)
        {
            return character == ' ' || character == 'n' || character == 't';
        }
    }
}