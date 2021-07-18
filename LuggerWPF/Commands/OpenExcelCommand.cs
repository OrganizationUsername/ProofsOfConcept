using System;
using System.Windows.Input;
using LuggerWPF;

namespace Commands.LuggerWPF
{
    public class OpenExcelCommand : ICommand
    {
        private readonly MainVm _mainVm;

        public OpenExcelCommand(MainVm mainVm)
        {
            _mainVm = mainVm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object circleRadioButton)
        {
            //Load from Excel
        }

        public event EventHandler CanExecuteChanged;
    }
}