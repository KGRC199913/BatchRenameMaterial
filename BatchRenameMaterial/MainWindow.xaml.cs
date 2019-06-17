using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
            SetStateRulePositionControl(false);

            // TEST AREA
            //TO BE DELETE
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
            processors.Add(
                new StringReplacer()
                {
                    Arg = new StringReplaceArg()
                    {
                        ReplacePattern = @"file",
                        ReplaceTarget = "f",
                        IgnoreCase = false
                    }
                });

            //
        }

        private void UpdateNewName()
        {
            // Parallel process the file name to new name
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
            // TODO: Add file rename
        }

        private void SaveRulesButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: create a dialog to get name/location
            //TODO: get preset name/location from user (save to saveLoc)
            string saveLoc = "./test.preset";
            // Serialize processors
            IFormatter formatter = new BinaryFormatter();
            Stream fstream = null;
            try
            {
                fstream = new FileStream(saveLoc, FileMode.CreateNew, FileAccess.Write);
            }
            catch (IOException ex)
            {
                // Preset is already exist
                //TODO: ask user to re-enter new preset name/location OR ask user to override
            }

            if (fstream != null)
                formatter.Serialize(fstream, processors);
            else
            {
                //TODO: announce saving preset failure to user
            }
        }

        private void LoadRulesButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: create a dialog to get name/location
            //TODO: get preset name/location from user (save to loadLoc)
            string loadLoc = "./test.preset";
            //Deserialize processors
            IFormatter formatter = new BinaryFormatter();
            Stream fstream = null;
            try
            {
                fstream = new FileStream(loadLoc, FileMode.Open, FileAccess.Read);
            }
            catch (IOException ex)
            {
                // Preset is not exist
                // TODO: ask user to re-enter preset name/location
            }

            if (fstream != null)
            {
                processors = (BindingList<IStringProcessor>)formatter.Deserialize(fstream);
            }
            else
            {
                //TODO: annound loading preset failure to user
            }
        }

        private void RemoveThisRuleButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button).DataContext as IStringProcessor;
            if (item != null)
            {
                processors.Remove(item);
            }
        }

        private void AddRuleButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Add config dialog
            //TODO: get Args and rule type
            //TODO: Create an Enum for types
            //TODO: set type and arg base on user input
            int type = 0;
            object arg = null;
            IStringProcessor processor = null;

            // WILL BE DELETE
            //TEST AREA
            arg = new StringReplaceArg()
            {
                ReplacePattern = "01",
                ReplaceTarget = "Cat"
            };
            //

            // Create correct type of string processor
            switch (type) 
            {
                case 0: // 0 is String regex replacer // SUBJECT OT BE CHANGED
                    processor = new StringReplacer()
                    {
                        Arg = arg as StringReplaceArg
                    };
                    break;

                default:
                    break;
            }

            processors.Add(processor);
        }

        private void SwapListPosition<T>(int first, int second, Collection<T> list)
        {
            var temp = list[first];
            list[first] = list[second];
            list[second] = temp;
        }

        private void RulesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rulesListView.SelectedIndex < 0)
            {
                SetStateRulePositionControl(false);
            }
            else
            {
                SetStateRulePositionControl(true);
            }

            if (rulesListView.SelectedIndex == (processors.Count - 1))
            {
                ruleDownMostButton.IsEnabled = ruleDownButton.IsEnabled = false;
            } else
            {
                if (rulesListView.SelectedIndex == 0)
                {
                    ruleUpMostButton.IsEnabled = ruleUpButton.IsEnabled = false;
                }
            }
        }

        private void SetStateRulePositionControl(bool isEnable)
        {
            ruleUpButton.IsEnabled = 
                ruleUpMostButton.IsEnabled =
                    ruleDownButton.IsEnabled =
                        ruleDownMostButton.IsEnabled = isEnable;
        }

        private void RuleUpButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = rulesListView.SelectedIndex;
            SwapListPosition(selected, selected - 1, processors);
            rulesListView.SelectedIndex = selected - 1;
        }

        private void RuleDownButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = rulesListView.SelectedIndex;
            SwapListPosition(selected, selected + 1, processors);
            rulesListView.SelectedIndex = selected + 1;
        }

        private void RuleUpMostButton_Click(object sender, RoutedEventArgs e)
        {
            var item = processors[rulesListView.SelectedIndex];
            processors.Remove(item);
            processors.Insert(0, item);
            rulesListView.SelectedIndex = 0;
        }

        private void RuleDownMostButton_Click(object sender, RoutedEventArgs e)
        {
            var item = processors[rulesListView.SelectedIndex];
            processors.Remove(item);
            processors.Add(item);
            rulesListView.SelectedIndex = processors.Count - 1;
        }
    }
}
