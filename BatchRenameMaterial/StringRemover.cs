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
    class StringRemover : IStringProcessor
    {
        private StringRemoveArg removeRule;

        public StringRemoveArg Arg { get => removeRule; set => removeRule = value; }

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
            var result = haystack.Remove(removeRule.StartIndex, removeRule.CharNumToDel);
            return result;
        }
    }
}
