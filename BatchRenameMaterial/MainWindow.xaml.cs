using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<File> files = new ObservableCollection<File>();
        BindingList<IStringProcessor> processors = new BindingList<IStringProcessor>();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.filesDataGrid.DataContext = files;
            this.rulesListView.DataContext = processors;

            // TEST AREA
            files.Add(
                new File()
                {
                    Name = "Test_file",
                    Path = "C:/FAKE"
                });
            files.Add(
                new File()
                {
                    Name = "Test_file01",
                    Path = "C:/FAKE"
                });
            processors.Add(
                new StringReplacer()
                {
                    Arg = new StringReplaceArg()
                    {
                        ReplacePattern = @"test",
                        ReplaceTarget = "meow",
                        IgnoreCase = true
                    }
                });
            //
        }

        private void UpdateNewName()
        {
            Parallel.ForEach(files, file =>
            {
                file.NewName = file.Name;
                foreach (var processor in processors)
                {
                    file.NewName = processor.Process(file.NewName);
                }
            });
        }
        // SUBJECT TO BE CHANGED
        private void StartRenameButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateNewName();
        }
    }
}
