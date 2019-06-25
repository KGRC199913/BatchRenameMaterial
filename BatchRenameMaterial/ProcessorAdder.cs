using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BatchRenameMaterial
{
    class ProcessorAdder : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
            => true;

        public void Execute(object parameter)
        {
            ProcessorType processorType = (ProcessorType)parameter;

            DialogType dialogType = MainWindow.GetDialogTypeFromProcessorType(processorType);
            object arg = null;
            if (dialogType != DialogType.NoDialog)
            {
                ConfigDialog cfDialog = new ConfigDialog(dialogType);
                if (cfDialog.ShowDialog() == true)
                {
                    arg = cfDialog.ArgReturn;
                }
                else
                {
                    return;
                }
            }

            MainWindow.AddCard(processorType, arg);
        }
    }
}
