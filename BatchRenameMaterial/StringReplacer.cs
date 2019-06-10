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
    class StringReplacer : IStringProcessor
    {
        /// <summary>
        ///     Replace tokens match with regex.
        /// </summary>
        /// <param name="haystack">
        ///     String to be modified
        /// </param>
        /// <param name="arg">
        ///     @<see cref="StringReplaceArg"/>
        /// </param>
        /// <returns>
        ///     A modified string if arg is valid. The same string if invalid arg.
        /// </returns>
        public string Process(string haystack, object arg)
        {
            var replaceRule = arg as StringReplaceArg;
            if (replaceRule == null)
                return haystack;
            var result = Regex.Replace(haystack, replaceRule.ReplacePattern, replaceRule.ReplaceTarget);
            return result;
        }
    }
}
