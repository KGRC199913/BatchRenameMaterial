using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <summary>
    /// Name Normalizer/
    /// </summary>
    [Serializable]
    class StringNameNormalizer : IstringProcessor
    {
        public string Decription 
            => $"Fullname normalize.";
        
        ///<summary>
        ///      Normalize the name (string).
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

            result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result.ToLower());
        }
    }
}