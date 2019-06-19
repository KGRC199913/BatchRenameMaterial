using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BatchRenameMaterial
{
    /// <summary>
    /// Interaction logic for ReplacerConfigUserControl.xaml
    /// </summary>
    public partial class ReplacerConfigUserControl : UserControl, IReturnValue
    {
        private StringReplaceArg arg;
        public ReplacerConfigUserControl()
        {
            InitializeComponent();
            arg = new StringReplaceArg();
            this.DataContext = arg;
        }

        public ReplacerConfigUserControl(StringReplaceArg argument)
        {
            InitializeComponent();
            arg = argument;
            this.DataContext = arg;
        } 

        public object ReturnValue => arg;
    }
}
