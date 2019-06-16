﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    class File : INotifyPropertyChanged
    {
        private string name;
        private string newName;
        private string path;
        private string error;

        public string Name
        {
            get => name; set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string NewName {
            get => newName;
            set {
                newName = value;
                OnPropertyChanged("NewName");
            }
        }
        public string Path {
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
