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
    /// Interaction logic for RemoverConfigUserControl.xaml
    /// </summary>
    public partial class RemoverConfigUserControl : UserControl, IReturnValue
    {
        private StringRemoveArg arg;
        public RemoverConfigUserControl()
        {
            InitializeComponent();
            arg = new StringRemoveArg();
            this.DataContext = arg;
        }

        public RemoverConfigUserControl(StringRemoveArg argument)
        {
            InitializeComponent();
            arg = argument;
            this.DataContext = arg;
        }

        public object ReturnValue => arg;
    }
}
