using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    [Serializable]
    class StringUppercaserAll : IStringProcessor
    {
        public string Description => "Uppercase the file/folder's name";

        public string Process(string haystack)
        {
            return haystack.ToUpper();
        }
    }
}
