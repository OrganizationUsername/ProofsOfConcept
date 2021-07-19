using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LuggerWPF;

namespace Commands.LuggerWPF
{
    public class AddAssemblyCommand : ICommand
    {
        private readonly MainVm _mainVm;

        public AddAssemblyCommand(MainVm mainVm)
        {
            _mainVm = mainVm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object circleRadioButton)
        {
            //add a new item to the list.
            _mainVm.Items.Add(new Item()
            {
                Owner = _mainVm,
                Name = $"Big {_mainVm.Items.Count + 1}",
                Ratio = 0.50,
                Thickness = 1.0,
                Demand = 500,
                Shapes = new ObservableCollection<IShape>(
                    new List<IShape>()
                    {
                        new Rectangle() {X = 0, Y = 0, Height = 90, Width = 150},
                        new Circle() {Diameter = 30, X = 20, Y = 20},
                    }),
            });
            _mainVm.RecalculateAllRatios();
        }

        public event EventHandler CanExecuteChanged;
    }
}