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
using System.Windows.Shapes;

namespace BatchRenameMaterial
{
    public enum DialogType
    {
        NoDialog,
        ReplacerConfigDialog,
        RemoverConfigDialog,
        RegexCaseConfigDialog,
        CaseConfigDialog,
        RepostionConfigDialog,
        AddConfigDialog
    }

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ConfigDialog : Window
    {
        

        static private FrameworkElement createUserControl(DialogType type)
        {
            switch (type)
            {
                case DialogType.ReplacerConfigDialog:
                    return new ReplacerConfigUserControl();
                case DialogType.RemoverConfigDialog:
                    return new RemoverConfigUserControl();
                case DialogType.CaseConfigDialog:
                    return new CaserConfigUserControl();
                case DialogType.RegexCaseConfigDialog:
                    return new RegexCaseConfigUserControl();
                case DialogType.RepostionConfigDialog:
                    return new RepostionConfigUserControl();
                case DialogType.AddConfigDialog:
                    return new AddConfigUserControl();
                default:
                    return null;
            }
        }

        private FrameworkElement currentDialog;
        private object argReturn;

        public ConfigDialog(DialogType type)
        {
            InitializeComponent();
            ConfigWindow.DataContext = this;
            CurrentDialog = createUserControl(type);
        }

        public object ArgReturn { get => argReturn; }
        public FrameworkElement CurrentDialog
        {
            get => currentDialog;
            set
            {
                currentDialog = value;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            argReturn = (currentDialog as IReturnValue).ReturnValue;
            this.DialogResult = true;
            MainWindow.UpdateNewName();
        }

        private void ConfigWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
