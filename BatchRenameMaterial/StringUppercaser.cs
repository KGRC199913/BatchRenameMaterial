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

        private StringCaseArg uppercaseRule;

        public StringCaseArg Arg { get => uppercaseRule; set => uppercaseRule = value; }


        public string Description
            => $"Uppercase from {uppercaseRule.StartIndex} to {uppercaseRule.EndIndex}";

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
            var endIndex = uppercaseRule.EndIndex;
            if (endIndex >= haystack.Length)
                endIndex = haystack.Length - 1;
            var result = new StringBuilder(haystack.Substring(0, uppercaseRule.StartIndex))
                            .Append(haystack.Substring(uppercaseRule.StartIndex, endIndex - uppercaseRule.StartIndex + 1).ToUpper())
                            .Append(haystack.Substring(endIndex + 1, haystack.Length - endIndex - 1))
                            .ToString();
            return result;
        }

    }
}