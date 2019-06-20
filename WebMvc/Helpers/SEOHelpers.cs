using iThinking.Common;
using System;
using System.Collections.Generic;

namespace WebMvc.Helpers
{
    public static class SEOHelpers
    {
        public static string GetSiteDescription(string name, string author, string content)
        {
            string result = "";
            if (!string.IsNullOrEmpty(author))
                result += "lời bài hát " + name + " - " + author;
            else
                result += "lời bài hát " + name;

            if (!string.IsNullOrEmpty(content))
            {
                result += " - " + CropWholeWords(content, 80);
            }

            return result;
        }

        public static string GetSiteKeywords(string name, string author, string content)
        {
            string result = "";
            if (!string.IsNullOrEmpty(author))
            {
                result += "lời bài hát " + name + " - " + author;
            }
            else
            {
                result += "lời bài hát " + name;
            }
            result += ", ca khúc " + name;
            result += ", karaoke " + name;
            result += ", mã số " + name;
            result += ", remix " + name;
            result += ", " + name + " remix";
            result += ", " + name + " lyrics";
            result += ", lyrics " + name;

            return result;
        }

        private static readonly HashSet<char> DefaultNonWordCharacters = new HashSet<char> { ',', '.', ':', ';' };

        public static string CropWholeWords(string valueHtml, int length, HashSet<char> nonWordCharacters = null)
        {
            if (string.IsNullOrWhiteSpace(valueHtml))
                valueHtml = "";
            string value = HtmlToText.ConvertHtml(valueHtml).Replace("\r", "").Replace("\n", "");
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

            return result;
        }

        private static bool IsWhitespace(this char character)
        {
            return character == ' ' || character == 'n' || character == 't';
        }
    }
}