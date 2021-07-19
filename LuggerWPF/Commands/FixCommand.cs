using System;
using System.Windows.Controls;
using System.Windows.Input;
using LuggerWPF;

namespace Commands.LuggerWPF
{
    public class FixCommand : ICommand
    {
        private readonly MainVm _mainVm;

        /// <summary>
        /// Need to get a list of acceptable thicknesses, 
        /// </summary>
        /// <param name="mainVm"></param>
        public FixCommand(MainVm mainVm)
        {
            _mainVm = mainVm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //I could have it pop up a window or sth with a textbox.





            //Maybe parameter could have the number of final sizes to have.

            //ok, so a button is pressed
            //let's hit up all MainVM's List<Item>
            foreach (var item in _mainVm.Items)
            {
                item.Thickness = Math.Ceiling(item.Ratio * item.Thickness / 0.25) * 0.25;
            }
            //take the ratios * existing thickness to be the minimum required thickness

            //Maybe I need to get a list of thicknesses
            //Nah, I'll just use 1/4" increments.
            //throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;
    }
}