using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Rendezvous.Utilities
{    
    /// <summary>
    /// Summary description for Censor
    /// </summary>
    public class Censor
    {
        public IList<string> CensoredWords { get; private set; }

        public Censor(IEnumerable<string> censoredWords)
        {

            if (censoredWords == null)

                throw new ArgumentNullException("censoredWords");

            CensoredWords = new List<string>(censoredWords);

        }

        public bool ContainsCensorText(string text)
        {
            if (text == null)

                throw new ArgumentNullException("text");

            foreach (string censoredWord in CensoredWords)
            {
                string regularExpression = ToRegexPattern(censoredWord);

                if (Regex.Match(text, regularExpression, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant).Success)
                {
                    return true;
                }
            }
                return false;
        }

        public string CensorText(string text)
        {

            if (text == null)

                throw new ArgumentNullException("text");

            string censoredText = text;

            foreach (string censoredWord in CensoredWords)
            {

                string regularExpression = ToRegexPattern(censoredWord);



                censoredText = Regex.Replace(censoredText, regularExpression, StarCensoredMatch,

                  RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

            }

            return censoredText;

        }

        private static string StarCensoredMatch(Match m)
        {

            string word = m.Captures[0].Value;

            return new string('*', word.Length);

        }

        private string ToRegexPattern(string wildcardSearch)
        {

            string regexPattern = Regex.Escape(wildcardSearch);



            regexPattern = regexPattern.Replace(@"\*", ".*?");

            regexPattern = regexPattern.Replace(@"\?", ".");



            if (regexPattern.StartsWith(".*?"))
            {

                regexPattern = regexPattern.Substring(3);

                regexPattern = @"(^\b)*?" + regexPattern;

            }

            regexPattern = @"\b" + regexPattern + @"\b";

            return regexPattern;

        }
    }
}
