using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <summary>
    /// A non-Regex StringRemover.
    /// </summary>
    [Serializable]
    class StringRemover : IStringProcessor
    {
        private StringRemoveArg removeRule;

        public StringRemoveArg Arg { get => removeRule; set => removeRule = value; }

        public string Description
            => $"Remove {removeRule.CharNumToDel} characters starting from {removeRule.StartIndex}";

        public bool ApplyToExtension { get; set; }

        /// <summary>
        ///      Remove a number of characters in a string.
        /// </summary>
        /// <param name="haystack">
        ///     String to be modified.
        /// </param>
        /// <returns>
        ///    A modified string if arg is valid. The same string if invalid arg.
        /// </returns>
        public string Process(string haystack)
        { 
            if (removeRule == null)
                return haystack;

            var startIndex = removeRule.StartIndex;
            var delCount = removeRule.CharNumToDel;
            if (delCount + startIndex - 1 >= haystack.Length)
                delCount = haystack.Length - startIndex;

            var result = haystack.Remove(startIndex, delCount);
            return result;
        }
    }
}
