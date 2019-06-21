using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <summary>
    ///     Argument for Non-regex @<see cref="StringUpperCaser"/> and @<see cref="StringLowerCaser"/>.
    /// </summary>
    [Serializable]
    public class StringCaseArg
    {
        /// <summary>
        ///     Begin postion for Uppercasing (First character is [0])
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        ///    Last postion for Uppercasing (Last character is [length - 1])
        /// </summary>
        public int EndIndex { get; set; }

    }
}
