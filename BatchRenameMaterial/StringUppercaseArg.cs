using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <summary>
    ///     Argument for Non-regex @<see cref="StringUppercaser"/>.
    /// </summary>
    [Serializable]
    public class StringUppercaseArg
    {
        /// <summary>
        ///     Begin postion for Uppercasing (First character is [0])
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        ///    Last postion for Uppercasing (Last character is [length])
        /// </summary>
        public int EndIndex {get; set;}

    }
}
