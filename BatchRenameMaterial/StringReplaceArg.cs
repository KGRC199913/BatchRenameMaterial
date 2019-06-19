using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <summary>
    /// Argrument for @<see cref="StringReplacer"/>.
    /// </summary>
    [Serializable]
    public class StringReplaceArg
    {
        /// <summary>
        ///     String will be change to this.
        /// </summary>
        public string ReplaceTarget { get; set; }
        /// <summary>
        ///     A Regex to match needles in the haystack.
        /// </summary>
        public string ReplacePattern { get; set; }

        public bool IgnoreCase { get; set; }
    }
}
