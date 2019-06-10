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
        /// <summary>
        ///      Remove a number of characters in a string.
        /// </summary>
        /// <param name="haystack">
        ///     String to be modified.
        /// </param>
        /// <param name="arg">
        ///     @<see cref="StringRemoveArg"/>
        /// </param>
        /// <returns>
        ///    A modified string if arg is valid. The same string if invalid arg.
        /// </returns>
        public string Process(string haystack, object arg)
        {
            var removeRule = arg as StringRemoveArg;
            if (removeRule == null)
                return haystack;
            var result = haystack.Remove(removeRule.StartIndex, removeRule.CharNumToDel);
            return result;
        }
    }
}
