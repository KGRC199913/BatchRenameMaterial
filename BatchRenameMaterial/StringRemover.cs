using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    class StringRemover : IStringProcessor
    {
        public string Process(string haystack, object arg)
        {
            var removeRule = arg as StringRemoveArg;
            return haystack.Remove(removeRule.StartIndex, removeRule.CharNumToDel);
        }
    }
}
