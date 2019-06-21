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
    class StringTrim : IStringProcessor
    {
        public string Description
            => $"Remove all leading + trailing white space";

        /// <summary>
        ///      Trim string (remove all leading + trailing white space)
        /// </summary>
        /// <param name="haystack">
        ///     String to be modified.
        /// </param>
        /// <returns>
        ///    A modified string.
        /// </returns>
        public string Process(string haystack)
        {
            var result = haystack.Trim();
            return result;
        }

    }
}