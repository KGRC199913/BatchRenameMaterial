using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <summary>
    ///     Argument for Non-regex @<see cref="StringLowercaser"/>.
    /// </summary>
    [Serializable]
    public class StringLowercaseArg
    {
        /// <summary>
        ///     Begin postion for Lowercasing (First character is [0])
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        ///    Last postion for Lowercasing (Last character is [length])
        /// </summary>
        public int EndIndex {get; set;}

    }
}
