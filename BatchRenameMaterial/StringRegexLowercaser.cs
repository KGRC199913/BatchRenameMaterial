using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <summary>
    /// Regex Lowercase.
    /// </summary>
    [Serializable]
    class StringRegexLowercaser : IStringProcessor
    {
        private StringRegexCaseArg lowerCaseRule;

        public StringRegexCaseArg Arg{get=>lowerCaseRule;set=>lowerCaseRule =value;}
        public string Description
            => $"Lowercase needles match the pattern \"\\{lowerCaseRule}\"";

        /// <summary>
        ///     Lowercase tokens match with regex.
        /// </summary>
        /// <param name="haystack">
        ///     String to be modified
        /// </param>
        /// <returns>
        ///     A modified string if arg is valid. The same string if invalid arg.
        /// </returns>
        public string Process(string haystack)
        {
            if (lowerCaseRule.RegexPattern == null)
                return haystack;
            var regexMatch = Regex.IsMatch(haystack, lowerCaseRule.RegexPattern);
            if (!regexMatch)
                lowerCaseRule.IgnoreCase = true;  //

            var regexOps = lowerCaseRule.IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;
            var result = Regex.Replace(haystack, lowerCaseRule.RegexPattern, lowerCaseRule.RegexPattern.ToLower(),regexOps);
            return result;
        }
    }
}