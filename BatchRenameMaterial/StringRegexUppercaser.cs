using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <summary>
    /// Regex Uppercase.
    /// </summary>
    [Serializable]
    class StringRegexUppercaser : IStringProcessor
    {
        private StringRegexCaseArg upperCaseRule;

        public StringRegexCaseArg Arg { get => upperCaseRule; set => upperCaseRule = value; }
        public string Description
            => $"Uppercase needles match the pattern \"\\{upperCaseRule.RegexPattern}\"";

        public bool ApplyToExtension { get; set; }
        /// <summary>
        ///     Uppercase tokens match with regex.
        /// </summary>
        /// <param name="haystack">
        ///     String to be modified
        /// </param>
        /// <returns>
        ///     A modified string if arg is valid. The same string if invalid arg.
        /// </returns>
        public string Process(string haystack)
        {
            if (upperCaseRule.RegexPattern == null)
                return haystack;
            var regexMatch = Regex.IsMatch(haystack, upperCaseRule.RegexPattern);
            if (!regexMatch)
                upperCaseRule.IgnoreCase = true;  //

            var regexOps = upperCaseRule.IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;
            var result = Regex.Replace(haystack, upperCaseRule.RegexPattern, 
                m => m.Value.ToUpper(),
                regexOps);
            return result;
        }
    }
}