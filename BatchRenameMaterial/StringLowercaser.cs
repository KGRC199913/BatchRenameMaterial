using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <sumary>
    /// A non-Regex StringLowerCaser
    /// </sumary>
    [Serializable]
    class StringLowerCaser : IStringProcessor
    {
        private StringCaseArg lowercaseRule;

        public StringCaseArg Arg {get => lowercaseRule; set=> lowercaseRule = value; }

        public string Description
            => $"Lowercase from {lowercaseRule.StartIndex + 1} to {lowercaseRule.EndIndex + 1}";

        /// <summary>
        ///      Lowercasing from x to y in a string.
        /// </summary>
        /// <param name="haystack">
        ///     String to be modified.
        /// </param>
        /// <returns>
        ///    A modified string if arg is valid. Lower all character if not.
        /// </returns>
        public string Process(string haystack)
        {
            if (lowercaseRule.EndIndex >= haystack.Length)
                lowercaseRule.EndIndex = haystack.Length -1;
            var needToLower = haystack.Substring(lowercaseRule.StartIndex, lowercaseRule.EndIndex - lowercaseRule.StartIndex +1);
            var result = needToLower.ToLower();
            return result;
        }

    }
}