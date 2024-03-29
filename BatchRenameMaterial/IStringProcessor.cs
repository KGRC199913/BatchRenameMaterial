﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    /// <summary>
    /// a Processor to modified a string.
    /// </summary>
    public interface IStringProcessor
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
        bool ApplyToExtension { get; set; }
    }

    public enum ProcessorType
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
        StringRegexUppercaser,
        StringRepositioner,
        StringAdder,
        StringUppercaserAll,
        StringLowercaserAll
    }

    static class ProcessorTypeDetector
    {
        public static ProcessorType GetType(IStringProcessor processor)
        {
            if (processor is StringReplacer)
                return ProcessorType.StringReplacer;
            if (processor is StringRemover)
                return ProcessorType.StringRemover;
            if (processor is StringUpperCaser)
                return ProcessorType.StringUpperCaser;
            if (processor is StringLowerCaser)
                return ProcessorType.StringLowerCaser;
            if (processor is StringRegexUppercaser)
                return ProcessorType.StringRegexUppercaser;
            if (processor is StringRegexLowercaser)
                return ProcessorType.StringRegexLowercaser;
            if (processor is StringTrimer)
                return ProcessorType.StringTrimer;
            if (processor is StringNameNormalizer)
                return ProcessorType.StringNameNormalizer;
            if (processor is StringGUIDCreator)
                return ProcessorType.StringGUIDCreator;
            if (processor is StringRepositioner)
                return ProcessorType.StringRepositioner;
            if (processor is StringAdder)
                return ProcessorType.StringAdder;
            if (processor is StringUppercaserAll)
                return ProcessorType.StringUppercaserAll;
            if (processor is StringLowercaserAll)
                return ProcessorType.StringLowercaserAll;
            return ProcessorType.Null;
        }
    }
}
