using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <summary>
    /// a Processor to modified a string.
    /// </summary>
    interface IStringProcessor
    {
        /// <summary>
        ///     Commit the change.
        /// </summary>
        /// <param name="haystack">
        ///     A string to be modified
        /// </param>
        /// <returns>
        ///     result string.
        /// </returns>
        string Process(string haystack);
        string Description { get; }
    }

    enum ProcessorType
    {
        Null,
        StringReplacer,
        StringRemover,
        StringTrimer,
        StringUpperCaser,
        StringLowerCaser,
        StringNameNormalizer,
        StringGUIDCreator,
        StringRegexLowercaser,
        StringRegexUppercaser

    }
}
