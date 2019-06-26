using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    [Serializable]
    class StringRepositionArg
    {
        public string Pattern { set; get; }

        public int Position { set; get; }

        public bool IgnoreCase { get; set; }
    }
}
