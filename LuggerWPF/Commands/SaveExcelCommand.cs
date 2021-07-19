using System;
using System.Windows.Input;
using LuggerWPF;

namespace Commands.LuggerWPF
{
    public class SaveExcelCommand : ICommand
    {
        private readonly MainVm _mainVm;

        public SaveExcelCommand(MainVm mainVm)
        {
            _mainVm = mainVm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object circleRadioButton)
        {
            //Save to Excel
            //Figure out a format...
            //For right now, all circles in one sheet
            //Rectangles in another
            //Parameters in another
            //Maybe also save the ratio, but don't read it in.
        }

        public event EventHandler CanExecuteChanged;
    }
}