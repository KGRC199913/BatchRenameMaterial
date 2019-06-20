using Microsoft.Win32;
using System;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

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
            this.fileCountStackPanel.DataContext = files;
            SetStateRulePositionControl(false);

            // TEST AREA
            //TO BE DELETE
            files.Add(
                new File()
                {
                    Name = "Test_file",
                    Path = "C:/FAKE",
                    IsFile = true
                });
            files.Add(
                new File()
                {
                    Name = "Test_file01",
                    Path = "C:/FAKE",
                    IsFile = true
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

        /// <summary>
        /// UpdateNewName for all name in files list.
        /// </summary>
        private void UpdateNewName()
        {
            foreach (var file in files)
            {
                file.NewName = file.Name;
                foreach (var processor in processors)
                {
                    file.NewName = processor.Process(file.NewName);
                }
            };
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
            //TODO: let user choose type

            //TODO: Add config dialog
            DialogType type = DialogType.RemoverConfigDialog;
            object arg = null;

            ConfigDialog cfDialog = new ConfigDialog(type);
            if (cfDialog.ShowDialog() == true)
            {
                arg = cfDialog.ArgReturn;
            }
            else
            {
                return;
            }
            // set type and arg base on user input

            //TODO: get Args and rule type
            //TODO: Create an Enum for types

            IStringProcessor processor = null;

            // Create correct type of string processor
            switch (type)
            {
                case DialogType.ReplacerConfigDialog: // 0 is String regex replacer // SUBJECT OT BE CHANGED
                    processor = new StringReplacer()
                    {
                        Arg = arg as StringReplaceArg
                    };
                    break;
                case DialogType.RemoverConfigDialog:
                    processor = new StringRemover()
                    {
                        Arg = arg as StringRemoveArg
                    };
                    break;
                default:
                    break;
            }

            processors.Add(processor);
        }


        /// <summary>
        /// Swap item position in a collection<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">
        /// first item's index
        /// </param>
        /// <param name="second">
        /// second item's index
        /// </param>
        /// <param name="list"></param>
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
            }
            else
            {
                if (rulesListView.SelectedIndex == 0)
                {
                    ruleUpMostButton.IsEnabled = ruleUpButton.IsEnabled = false;
                }
            }
        }

        /// <summary>
        /// Change all positioning button to enable/disable
        /// </summary>
        /// <param name="isEnable">
        /// State to be set for buttons
        /// </param>
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

        private bool isAddedFile(string name, string path)
        {
            File newfile = new File()
            {
                Name = name,
                Path = path
            };
            foreach (var file in files)
            {
                if (file.Equals(newfile)) return true;
            }
            return false;
        }

        private void AddFilesButton_Click(object sender, RoutedEventArgs e)
        {
            filesDataGrid.SelectedIndex = -1;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "All File|*.*";

            string fileName, filePath;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var fileFullName in openFileDialog.FileNames)
                {
                    fileName = System.IO.Path.GetFileName(fileFullName);
                    filePath = System.IO.Path.GetDirectoryName(fileFullName);
                    if (isAddedFile(fileName, filePath)) continue;
                    files.Add(new File()
                    {
                        Name = fileName,
                        Path = filePath,
                        IsFile = true
                    });
                }
            }
            else
            {
                return;
            }
        }

        private void AddFoldersButton_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog();
            fileDialog.IsFolderPicker = true;
            fileDialog.Multiselect = true;

            string[] subfolders;
            string folderName;
            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                foreach (var folderFullName in fileDialog.FileNames)
                {
                    subfolders = Directory.GetDirectories(folderFullName);
                    foreach (var subfolder in subfolders)
                    {
                        folderName = new DirectoryInfo(subfolder).Name;
                        if (isAddedFile(folderName, folderFullName)) continue;
                        files.Add(
                            new File()
                            {
                                Name = folderName,
                                Path = folderFullName,
                                IsFile = false
                            });
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void IsDarkModeToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml");
            Application.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Purple.xaml");
            Application.Current.Resources.MergedDictionaries[3].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.DeepPurple.xaml");
        }

        private void IsDarkModeToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml");
            Application.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Teal.xaml");
            Application.Current.Resources.MergedDictionaries[3].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Yellow.xaml");
        }

        private void FilesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filesDataGrid.SelectedIndex < 0)
            {
                SetEnableFilePositioningButtons(false);
            }
            else
            {
                SetEnableFilePositioningButtons(true);
            }

            if (filesDataGrid.SelectedIndex == 0)
            {
                fileUpButton.IsEnabled = fileUpMostButton.IsEnabled = false;
            }
            else
            {
                if (filesDataGrid.SelectedIndex == (files.Count - 1))
                {
                    fileDownButton.IsEnabled = fileDownMostButton.IsEnabled = false;
                }
            }
        }

        private void SetEnableFilePositioningButtons(bool isEnable)
        {
            fileUpButton.IsEnabled =
                                fileDownButton.IsEnabled =
                                    fileUpMostButton.IsEnabled =
                                        fileDownMostButton.IsEnabled = isEnable;
        }

        private void FileUpButton_Click(object sender, RoutedEventArgs e)
        {
            int currentRow = filesDataGrid.SelectedIndex;
            SwapListPosition(currentRow, currentRow - 1, files);
            filesDataGrid.SelectedIndex = currentRow - 1;
        }

        private void FileDownButton_Click(object sender, RoutedEventArgs e)
        {
            int currentRow = filesDataGrid.SelectedIndex;
            SwapListPosition(currentRow, currentRow + 1, files);
            filesDataGrid.SelectedIndex = currentRow + 1;
        }

        private void FileUpMostButton_Click(object sender, RoutedEventArgs e)
        {
            var item = files[filesDataGrid.SelectedIndex];
            files.Remove(item);
            files.Insert(0, item);
            filesDataGrid.SelectedIndex = 0;
        }

        private void FileDownMostButton_Click(object sender, RoutedEventArgs e)
        {
            var item = files[filesDataGrid.SelectedIndex];
            files.Remove(item);
            files.Add(item);
            filesDataGrid.SelectedIndex = files.Count - 1;
        }
    }
}
