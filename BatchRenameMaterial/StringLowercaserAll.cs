using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    [Serializable]
    class StringLowercaserAll : IStringProcessor
    {
        public string Description => "Lowercase the file/folder's name";

        public bool ApplyToExtension { get; set; }

        public string Process(string haystack)
        {
            return haystack.ToLower();
        }
    }
}
