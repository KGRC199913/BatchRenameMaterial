using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <summary>
    ///     Argument for Non-regex @<see cref="StringRemover"/>.
    /// </summary>
    [Serializable]
    public class StringRemoveArg
    {
        /// <summary>
        ///     Begin postion of the deletion.
        /// </summary>
        public int StartIndex { get; set; }
        /// <summary>
        ///     Number of character to be deleted.
        /// </summary>
        public int CharNumToDel { get; set; }
    }
}
