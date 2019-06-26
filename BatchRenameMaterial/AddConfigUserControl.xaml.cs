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
    /// Interaction logic for AddConfigUserControl.xaml
    /// </summary>
    public partial class AddConfigUserControl : UserControl, IReturnValue
    {
        private StringAdderArg arg;
        public AddConfigUserControl()
        {
            InitializeComponent();
            arg = new StringAdderArg();
            this.DataContext = arg;
        }

        public object ReturnValue 
            => arg;
    }
}
