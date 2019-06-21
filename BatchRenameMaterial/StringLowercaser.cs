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
        private StringCasingArg uppercaseRule;

        public StringCasingArg Arg {get =>uppercaseRule;set=>uppercaseRule=value; }

        public string Description
            => $"Lowercase from {uppercaseRule.StartIndex + 1} to {uppercaseRule.EndIndex + 1}";

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
            if (uppercaseRule.EndIndex >= haystack.Length)
                uppercaseRule.EndIndex = haystack.Length -1;
            var needToLower = haystack.Substring(uppercaseRule.StartIndex, uppercaseRule.EndIndex - uppercaseRule.StartIndex +1);
            var result = needToLower.ToLower();
            return result;
        }

    }
}