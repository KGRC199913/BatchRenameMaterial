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
using System.Linq;


namespace BatchRenameMaterial
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        static ObservableCollection<File> files = new ObservableCollection<File>();
        public static BindingList<IStringProcessor> processors = new BindingList<IStringProcessor>();
        BindingList<ProcessorViewModel> processorWrapers = new BindingList<ProcessorViewModel>()
        {
            new ProcessorViewModel()
            {
                IconKind = "Hashtag",
                ProcessorName = "Create GUID name",
                Commander = Adder,
                PType = ProcessorType.StringGUIDCreator
            },
            new ProcessorViewModel()
            {
                IconKind = "FormatLetterCaseUpper",
                ProcessorName = "Non-regex Uppercaser",
                Commander = Adder,
                PType = ProcessorType.StringUpperCaser
            },
            new ProcessorViewModel()
            {
                IconKind = "FormatLetterCaseLower",
                ProcessorName = "Non-regex Lowercaser",
                Commander = Adder,
                PType = ProcessorType.StringLowerCaser
            },
            new ProcessorViewModel()
            {
                IconKind = "ContentCut",
                ToolTipsText = "Remove all leading + trailing white space",
                ProcessorName = "Trim",
                Commander = Adder,
                PType = ProcessorType.StringTrimer
            },
            new ProcessorViewModel()
            {
                IconKind = "" ,
                IconImg = "/icon/regexUpper_icon.png",
                ToolTipsText = "Uppercase All token match the regex",
                ProcessorName = "Regex Uppercase",
                Commander = Adder,
                PType = ProcessorType.StringRegexUppercaser
            },
            new ProcessorViewModel()
            {
                IconKind = "",
                IconImg = "/icon/regexLower_icon.png",
                ToolTipsText = "Uppercase All token match the regex",
                ProcessorName = "Regex Lowercase",
                Commander = Adder,
                PType = ProcessorType.StringRegexLowercaser
            },
            new ProcessorViewModel()
            {
                IconKind = "RenameBox",
                ProcessorName = "Name Normalize",
                Commander = Adder,
                PType = ProcessorType.StringNameNormalizer
            },
            new ProcessorViewModel()
            {
                IconKind = "FindReplace",
                ProcessorName = "Regex Replace",
                Commander = Adder,
                PType = ProcessorType.StringReplacer
            },
            new ProcessorViewModel()
            {
                IconKind = "RemoveCircle",
                ProcessorName = "Non-regex Remove",
                Commander = Adder,
                PType = ProcessorType.StringRemover
            },
            new ProcessorViewModel()
            {
                IconKind = "Wrap",
                ProcessorName = "Repositon",
                Commander = Adder,
                PType = ProcessorType.StringRepositioner
            },
            new ProcessorViewModel()
            {
                IconKind = "AddBox",
                ProcessorName = "Add Token",
                Commander = Adder,
                PType = ProcessorType.StringAdder
            }
        };
        static ProcessorAdder Adder = new ProcessorAdder();


        static DuplicateResolveType resolveType = DuplicateResolveType.KeepOldName;

        public static DuplicateResolveType ResolveType { get => resolveType; set => resolveType = value; }

        public static DialogType GetDialogTypeFromProcessorType(ProcessorType type)
        {
            switch (type)
            {
                case ProcessorType.StringNameNormalizer:
                case ProcessorType.StringTrimer:
                case ProcessorType.StringGUIDCreator:
                    return DialogType.NoDialog;
                case ProcessorType.StringLowerCaser:
                case ProcessorType.StringUpperCaser:
                    return DialogType.CaseConfigDialog;
                case ProcessorType.StringRegexLowercaser:
                case ProcessorType.StringRegexUppercaser:
                    return DialogType.RegexCaseConfigDialog;
                case ProcessorType.StringRemover:
                    return DialogType.RemoverConfigDialog;
                case ProcessorType.StringReplacer:
                    return DialogType.ReplacerConfigDialog;
                case ProcessorType.StringRepositioner:
                    return DialogType.RepostionConfigDialog;
                case ProcessorType.StringAdder:
                    return DialogType.AddConfigDialog;
                default:
                    return DialogType.NoDialog;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.filesDataGrid.DataContext = files;
            this.rulesListView.DataContext = processors;
            this.fileCountStackPanel.DataContext = files;
            this.ProcessorsHolderItemsControl.DataContext = processorWrapers;
            SetStateRulePositionControl(false);
            SetEnableFilePositioningButtons(false);
        }

        /// <summary>
        /// UpdateNewName for all name in files list.
        /// </summary>
        public static void UpdateNewName()
        {
            HashSet<String> fullNameList = new HashSet<string>();
            foreach (var file in files)
            {
                file.NewName = file.Name;
                foreach (var processor in processors)
                {
                    file.NewName = processor.Process(file.NewName);
                }

                if (!fullNameList.Contains(file.getNewFullName()))
                {
                    fullNameList.Add(file.getNewFullName());
                }
                else
                {
                    //option 1: keep old name
                    if (resolveType == DuplicateResolveType.KeepOldName)
                    {
                        file.NewName = file.Name;
                    }
                    else
                    {
                        //option 2: add number
                        while (fullNameList.Contains(file.getNewFullName()))
                            ++file.DuplicateCount;
                    }
                }

                if (file.NewName == "")
                {
                    file.Error = "New name is null";
                }
            };
        }

        private void StartRenameButton_Click(object sender, RoutedEventArgs e)
        {
            files.AsParallel().ForAll(i =>
            {
                // file
                if (i.IsFile == true)
                {
                    if (System.IO.File.Exists(i.getNewFullName()))
                    {
                        if (resolveType == DuplicateResolveType.KeepOldName)
                        {
                            i.NewName = i.Name;
                        }
                        else
                        {
                            while (System.IO.File.Exists(i.getNewFullName()))
                                ++i.DuplicateCount;
                        }
                    }
                    try
                    {
                        System.IO.File.Move(i.Path + "\\" + i.Name + i.Extension, i.getNewFullName());
                    }
                    catch (Exception ex)
                    {
                        i.Error = ex.Message;
                        i.NewName = i.Name;
                    }
                    i.Name = i.NewName;
                }
                // folder
                else
                {
                    if (Directory.Exists(i.getNewFullName()))
                    {
                        if (resolveType == DuplicateResolveType.KeepOldName)
                        {
                            i.NewName = i.Name;
                        }
                        else
                        {
                            while (Directory.Exists(i.getNewFullName()))
                                ++i.DuplicateCount;
                        }
                    }
                    try
                    {
                        Directory.Move(i.Path + "\\" + i.Name, i.getNewFullName());
                    }
                    catch (Exception ex)
                    {
                        i.Error = ex.Message;
                        i.NewName = i.Name;
                    }
                    i.Name = i.NewName;
                }
            });

            UpdateNewName();

        }

        private void SaveRulesButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: create a dialog to get name/location
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Preset file (*.preset)| *.preset";
            saveFileDialog.DefaultExt = "*.preset";
            saveFileDialog.OverwritePrompt = true;

            //TODO: get preset name/location from user (save to saveLoc)
            string saveLoc = "./test.preset";

            if (saveFileDialog.ShowDialog() == true)
            {
                saveLoc = saveFileDialog.FileName;
            }

            // Serialize processors
            IFormatter formatter = new BinaryFormatter();
            Stream fstream = null;
            try
            {
                fstream = new FileStream(saveLoc, FileMode.CreateNew, FileAccess.Write);
            }
            catch (IOException ex)
            {
                Logger.Error(ex, "SaveRulesButton");
                fstream = new FileStream(saveLoc, FileMode.Create, FileAccess.Write);
            }

            if (fstream != null)
                formatter.Serialize(fstream, processors);
            else
            {
                Logger.Debug("SaveRulesButton_Click: fstream is null");
                MessageBox.Show("Save preset failed.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void LoadRulesButton_Click(object sender, RoutedEventArgs e)
        {
            //create a dialog to get name/location
            //get preset name/location from user (save to loadLoc)
            string loadLoc = "./test.preset";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Preset file (*.preset)| *.preset";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                loadLoc = openFileDialog.FileName;
            }

            //Deserialize processors
            IFormatter formatter = new BinaryFormatter();
            Stream fstream = null;
            try
            {
                fstream = new FileStream(loadLoc, FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "LoadRuleButton");
            }

            if (fstream != null)
            {
                processors = (BindingList<IStringProcessor>)formatter.Deserialize(fstream);
                this.rulesListView.DataContext = processors;
            }
            else
            {
                Logger.Error("LoadRulesButton_Click: fstream is null");
                MessageBox.Show("Load preset failed.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateNewName();
        }

        private void RemoveThisRuleButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button).DataContext as IStringProcessor;
            if (item != null)
            {
                processors.Remove(item);
            }
            UpdateNewName();
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
            }

            if (rulesListView.SelectedIndex == 0)
            {
                ruleUpMostButton.IsEnabled = ruleUpButton.IsEnabled = false;
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
            var items = rulesListView.SelectedItems;
            List<IStringProcessor> tempItemsList = new List<IStringProcessor>();
            foreach (var item in items)
                tempItemsList.Add(item as IStringProcessor);

            int row;
            foreach (var item in tempItemsList)
            {
                try
                {
                    row = processors.IndexOf(item as IStringProcessor);
                    SwapListPosition(row, row - 1, processors);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        private void RuleDownButton_Click(object sender, RoutedEventArgs e)
        {
            var items = rulesListView.SelectedItems;
            List<IStringProcessor> tempItemsList = new List<IStringProcessor>();
            foreach (var item in items)
                tempItemsList.Add(item as IStringProcessor);

            int row;
            foreach (var item in tempItemsList)
            {
                try
                {
                    row = processors.IndexOf(item as IStringProcessor);
                    SwapListPosition(row, row + 1, processors);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        private void RuleUpMostButton_Click(object sender, RoutedEventArgs e)
        {
            var items = filesDataGrid.SelectedItems;
            List<IStringProcessor> tempItemsList = new List<IStringProcessor>();
            foreach (var item in items)
                tempItemsList.Add(item as IStringProcessor);

            foreach (var item in tempItemsList)
            {
                processors.Remove(item as IStringProcessor);
                processors.Insert(0, item as IStringProcessor);
            }
        }

        private void RuleDownMostButton_Click(object sender, RoutedEventArgs e)
        {
            var items = filesDataGrid.SelectedItems;
            List<IStringProcessor> tempItemsList = new List<IStringProcessor>();
            foreach (var item in items)
                tempItemsList.Add(item as IStringProcessor);

            foreach (var item in tempItemsList)
            {
                processors.Remove(item as IStringProcessor);
                processors.Add(item as IStringProcessor);
            }
        }

        private void AddFilesButton_Click(object sender, RoutedEventArgs e)
        {
            filesDataGrid.SelectedIndex = -1;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "All File|*.*";

            File newfile;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var fileFullName in openFileDialog.FileNames)
                {
                    newfile = new File()
                    {
                        Name = System.IO.Path.GetFileNameWithoutExtension(fileFullName),
                        Extension = System.IO.Path.GetExtension(fileFullName),
                        Path = System.IO.Path.GetDirectoryName(fileFullName),
                        IsFile = true
                    };

                    if (files.Contains(newfile))
                        continue;
                    files.Add(newfile);
                }
            }
            else
            {
                return;
            }

            UpdateNewName();
        }

        private void AddFoldersButton_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog();
            fileDialog.IsFolderPicker = true;
            fileDialog.Multiselect = true;

            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                File folder;
                foreach (var folderFullName in fileDialog.FileNames)
                {
                    try
                    {
                        folder = new File()
                        {
                            Name = new DirectoryInfo(folderFullName).Name,
                            IsFile = false,
                            Path = Directory.GetParent(folderFullName).FullName
                        };

                        if (files.Contains(folder))
                            continue;
                        files.Add(folder);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }
            else
            {
                return;
            }

            UpdateNewName();
        }

        private void IsDarkModeToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries[0].Source =
                new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml");
            Application.Current.Resources.MergedDictionaries[2].Source =
                new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Purple.xaml");
            Application.Current.Resources.MergedDictionaries[3].Source =
                new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.DeepPurple.xaml");
        }

        private void IsDarkModeToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries[0].Source =
                new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml");
            Application.Current.Resources.MergedDictionaries[2].Source =
                new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Teal.xaml");
            Application.Current.Resources.MergedDictionaries[3].Source =
                new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Yellow.xaml");
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
            if (filesDataGrid.SelectedIndex == (files.Count - 1))
            {
                fileDownButton.IsEnabled = fileDownMostButton.IsEnabled = false;
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
            var items = filesDataGrid.SelectedItems;
            List<File> tempItemsList = new List<File>();
            foreach (var item in items)
                tempItemsList.Add(item as File);

            int row;
            foreach (var item in tempItemsList)
            {
                try
                {
                    row = files.IndexOf(item as File);
                    SwapListPosition(row, row - 1, files);
                } catch (Exception ex)
                {
                    continue;
                }
            }
        }

        private void FileDownButton_Click(object sender, RoutedEventArgs e)
        {
            var items = filesDataGrid.SelectedItems;
            List<File> tempItemsList = new List<File>();
            foreach (var item in items)
                tempItemsList.Add(item as File);

            int row;
            foreach (var item in tempItemsList)
            {
                try
                {
                    row = files.IndexOf(item as File);
                    SwapListPosition(row, row + 1, files);
                } catch (Exception ex)
                {
                    continue;
                }
            }
        }

        private void FileUpMostButton_Click(object sender, RoutedEventArgs e)
        {
            var items = filesDataGrid.SelectedItems;
            List<File> tempItemsList = new List<File>();
            foreach (var item in items)
                tempItemsList.Add(item as File);

            foreach (var item in tempItemsList)
            {
                files.Remove(item as File);
                files.Insert(0, item as File);
            }
        }

        private void FileDownMostButton_Click(object sender, RoutedEventArgs e)
        {
            var items = filesDataGrid.SelectedItems;
            List<File> tempItemsList = new List<File>();
            foreach (var item in items)
                tempItemsList.Add(item as File);

            foreach (var item in tempItemsList)
            {
                files.Remove(item as File);
                files.Add(item as File);
            }
        }

        public static void AddCard(ProcessorType processorType, object arg)
        {
            IStringProcessor processor = null;

            // Create correct type of string processor
            processor = GetProcessor(processorType, arg);

            processors.Add(processor);
            MainWindow.UpdateNewName();

        }

        private static IStringProcessor GetProcessor(ProcessorType processorType,
                                                     object arg)
        {
            IStringProcessor processor = null;
            switch (processorType)
            {
                case ProcessorType.StringReplacer:
                    processor = new StringReplacer()
                    {
                        Arg = arg as StringReplaceArg
                    };
                    break;
                case ProcessorType.StringRemover:
                    processor = new StringRemover()
                    {
                        Arg = arg as StringRemoveArg
                    };
                    break;
                case ProcessorType.StringUpperCaser:
                    processor = new StringUpperCaser()
                    {
                        Arg = arg as StringCaseArg
                    };
                    break;
                case ProcessorType.StringLowerCaser:
                    processor = new StringLowerCaser()
                    {
                        Arg = arg as StringCaseArg
                    };
                    break;
                case ProcessorType.StringRegexLowercaser:
                    processor = new StringRegexLowercaser()
                    {
                        Arg = arg as StringRegexCaseArg
                    };
                    break;
                case ProcessorType.StringRegexUppercaser:
                    processor = new StringRegexUppercaser()
                    {
                        Arg = arg as StringRegexCaseArg
                    };
                    break;
                case ProcessorType.StringTrimer:
                    processor = new StringTrimer();
                    break;
                case ProcessorType.StringNameNormalizer:
                    processor = new StringNameNormalizer();
                    break;
                case ProcessorType.StringGUIDCreator:
                    processor = new StringGUIDCreator();
                    break;
                case ProcessorType.StringRepositioner:
                    processor = new StringRepositioner()
                    {
                        Arg = arg as StringRepositionArg,
                    };
                    break;
                case ProcessorType.StringAdder:
                    processor = new StringAdder()
                    {
                        Arg = arg as StringAdderArg,
                    };
                    break;
                default:
                    break;
            }

            return processor;
        }

        private void PopupBox_addRule_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border_BlurWhenPopupBoxEnter.Opacity = 0.85;

            Border_BlurWhenPopupBoxEnter.IsHitTestVisible = true;


        }

        private void PopupBox_addRule_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border_BlurWhenPopupBoxEnter.Opacity = 0;

            Border_BlurWhenPopupBoxEnter.IsHitTestVisible = false;

        }

        private void ConfigThisRuleButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (IStringProcessor)((Button)sender).DataContext;
            var processorType = ProcessorTypeDetector.GetType(item);
            var dialogType = GetDialogTypeFromProcessorType(processorType);
            ConfigDialog configDialog = new ConfigDialog(dialogType);
            if (configDialog.ShowDialog() == true)
            {
                var index = processors.IndexOf(item);
                IStringProcessor processor = GetProcessor(processorType, configDialog.ArgReturn);
                processors.Insert(index, processor);
                processors.RemoveAt(index + 1);
                rulesListView.SelectedIndex = index;
            }
            else
                return;
            UpdateNewName();
        }

        private void ResolveRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateNewName();
        }

        private void RemoveFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var items = filesDataGrid.SelectedItems;
            List<File> tempItemsList = new List<File>();
            foreach (var item in items)
                tempItemsList.Add(item as File);

            foreach (var item in tempItemsList)
            {
                try
                {
                    files.Remove(item);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }
    }
}
