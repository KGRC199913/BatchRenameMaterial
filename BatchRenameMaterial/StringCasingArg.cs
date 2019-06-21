using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <summary>
    ///     Argument for Non-regex @<see cref="StringLowercaser"> <see cref="StringUppercaser"> />.
    /// </summary>
    [Serializable]
    public class StringCasingArg
    {
        /// <summary>
        ///     Begin postion for Casing (First character is [0])
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        ///    Last postion for Casing (Last character is [length])
        /// </summary>
        public int EndIndex {get; set;}

    }
}
