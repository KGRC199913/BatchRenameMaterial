using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    class StringReplacer : IStringProcessor
    {
        public string Process(string haystack, object arg)
        {
            var replaceRule = arg as StringReplaceArg;
            return Regex.Replace(haystack, replaceRule.ReplacePattern, replaceRule.ReplaceTarget);
        }
    }
}
