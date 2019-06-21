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
    /// Interaction logic for CaserConfigUserControl.xaml
    /// </summary>
    public partial class CaserConfigUserControl : UserControl, IReturnValue
    {
        private StringCaseArg arg;
        public CaserConfigUserControl()
        {
            InitializeComponent();
            arg = new StringCaseArg();
            this.DataContext = arg;
        }

        public CaserConfigUserControl(StringCaseArg caseArg)
        {
            InitializeComponent();
            arg = caseArg;
            this.DataContext = arg;
        }

        public object ReturnValue => arg;
    }
}
