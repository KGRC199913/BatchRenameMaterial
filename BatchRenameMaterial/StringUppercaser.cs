using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <sumary>
    /// A non-Regex StringUpperCaser
    /// </sumary>
    [Serializable]
    class StringUpperCaser : IStringProcessor
    {
        private StringCasingArg uppercaseRule;

        public StringCasingArg Arg {get =>uppercaseRule;set=>uppercaseRule=value; }

        public string Description
            => $"Uppercase from {uppercaseRule.StartIndex + 1} to {uppercaseRule.EndIndex + 1}";

        /// <summary>
        ///      Uppercasing from x to y in a string.
        /// </summary>
        /// <param name="haystack">
        ///     String to be modified.
        /// </param>
        /// <returns>
        ///    A modified string if arg is valid. Upper all character if not.
        /// </returns>
        public string Process(string haystack)
        {
            if (uppercaseRule.EndIndex >= haystack.Length)
                uppercaseRule.EndIndex = haystack.Length -1;
            var needToUpper = haystack.Substring(uppercaseRule.StartIndex, uppercaseRule.EndIndex - uppercaseRule.StartIndex +1);
            var result = needToUpper.ToUpper();
            return result;
        }

    }
}