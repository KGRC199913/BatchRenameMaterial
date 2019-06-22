using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <summary>
    ///     Argument for <see cref="StringRegexUpperCaser"/> and @<see cref="StringRegexLowercaser"/>.
    /// </summary>
    [Serializable]
    public class StringRegexCaseArg
    {
        /// <summary>
        ///     Regex from User
        /// </summary>
        public string RegexPattern { get; set; }

        public bool IgnoreCase { get; set; }
    }
}
