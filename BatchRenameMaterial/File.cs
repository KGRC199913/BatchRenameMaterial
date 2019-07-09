using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    class File : INotifyPropertyChanged
    {
        public enum FileCase
        {
            None,
            AllUpper,
            AllLower
        }
        private string name;
        private string extension;
        private string newExtension;
        private string newName;
        private string path;
        private string error;
        private bool isFile;
        private int duplicateCount;
        private FileCase fcase;

        public string Name
        {
            get => name; set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string NewName
        {
            get => newName;
            set
            {
                newName = value;
                OnPropertyChanged("NewName");
            }
        }
        public string Path
        {
            get => path;
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }
        public string Error
        {
            get => error;
            set
            {
                error = value;
                OnPropertyChanged("Error");
            }
        }

        public bool IsFile
        {
            get => isFile;
            set
            {
                isFile = value;
                OnPropertyChanged("IsFile");
            }
        }

        public string Extension
        {
            get => extension;
            set
            {
                extension = value;
                OnPropertyChanged("Extension");
            }
        }

        public int DuplicateCount { get => duplicateCount; set => duplicateCount = value; }
        internal FileCase Fcase { get => fcase; set => fcase = value; }
        public string NewExtension {
            get => newExtension;
            set {
                newExtension = value;
                OnPropertyChanged("NewExtension");
            }
        }

        public override bool Equals(object obj)
        {
            try
            {
                File newfile = (File)obj;
                return name == newfile.name && path == newfile.path && isFile == newfile.isFile;
            }
            catch
            {
                return false;
            }
        }
        public string getNewFullName()
        {
            if (duplicateCount != 0)
                return path + "\\" + newName + " (" + duplicateCount.ToString() + ")" + newExtension;
            return path + "\\" + newName + newExtension;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
