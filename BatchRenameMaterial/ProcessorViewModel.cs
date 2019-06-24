using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BatchRenameMaterial
{
    class ProcessorViewModel
    {
        public string IconKind { get; set; }
        public string ProcessorName { get; set; }
        public string ToolTipsText { get; set; }
        public ICommand Commander { get; set; }
        public ProcessorType PType { get; set; }
    }
}
