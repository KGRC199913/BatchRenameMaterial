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
    /// Interaction logic for RegexCaseConfigUserControl.xaml
    /// </summary>
    public partial class RegexCaseConfigUserControl : UserControl, IReturnValue
    {
        private StringRegexCaseArg arg;
        public RegexCaseConfigUserControl()
        {
            InitializeComponent();
            arg = new StringRegexCaseArg();
            this.DataContext = arg;
        }

        public RegexCaseConfigUserControl(StringRegexCaseArg argument)
        {
            InitializeComponent();
            arg = argument;
            this.DataContext = arg;
        }

        public object ReturnValue => arg;
    }
}
