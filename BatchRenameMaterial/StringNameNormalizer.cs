using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <summary>
    /// Name Normalizer/
    /// </summary>
    [Serializable]
    class StringNameNormalizer : IStringProcessor
    {
        public string Description 
            => $"Fullname normalize.";
        
        ///<summary>
        ///      Normalize the name (string). Applied only to name rule that have ONLY the first character of each words capitalized.
        /// </summary>
        /// <param name="haystack">
        ///     String to be modified.
        /// </param>
        /// <returns>
        ///    A modify string.
        /// </returns>
        public string Process(string haystack)
        {
            var result = haystack.Trim(); 
            result = Regex.Replace(result, @"\s+", " ");
            result = Regex.Replace(result, "\b[a-z]", m => m.Value.ToUpper());

            return result;
        }
    }
}