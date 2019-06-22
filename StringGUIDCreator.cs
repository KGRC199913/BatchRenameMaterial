using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <summary>
    /// A non-Regex StringGUIDCreator
    /// </summary>
    [Serializable]
    class StringGUIDCreator : IStringProcessor
    {
        public string Decription
            => $"Create GUID for a string inputed.";

         /// <summary>
        ///      Create GUID for a string inputed..
        /// </summary>
        /// <param name="haystack">
        ///     String to be modified.
        /// </param>
        /// <returns>
        ///    A modify string (GUID).
        /// </returns>
        public string Process(string haystack)
            => Guid.NewGuid().ToString();
    }
}