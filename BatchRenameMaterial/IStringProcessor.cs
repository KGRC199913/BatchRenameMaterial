using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    interface IStringProcessor
    {
        string Process(string haystack, object arg);
    }
}
