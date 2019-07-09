using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <summary>
    /// A regex string replacer.
    /// </summary>
    [Serializable]
    class StringReplacer : IStringProcessor
    {
        private StringReplaceArg replaceRule;

        public StringReplaceArg Arg { get => replaceRule; set => replaceRule = value; }

        public string Description
            => $"Replace needles match the pattern \"\\{replaceRule.ReplacePattern}\" with \"{replaceRule.ReplaceTarget}\"";

        public bool ApplyToExtension { get; set; }
        /// <summary>
        ///     Replace tokens match with regex.
        /// </summary>
        /// <param name="haystack">
        ///     String to be modified
        /// </param>
        /// <returns>
        ///     A modified string if arg is valid. The same string if invalid arg.
        /// </returns>
        public string Process(string haystack)
        {
            if (replaceRule == null)
                return haystack;

            var regexOps = replaceRule.IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;
                
            var result = Regex.Replace(haystack, replaceRule.ReplacePattern, replaceRule.ReplaceTarget, regexOps);
            return result;
        }
    }
}
