using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using LuggerWPF;

namespace Commands.LuggerWPF
{
    public class DeleteShapeCommand : ICommand
    {
        private readonly MainVm _mainVm;

        public DeleteShapeCommand(MainVm mainVm)
        {
            _mainVm = mainVm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object shapeId)
        {
            if (shapeId is int id && _mainVm.SelectedItem.Shapes.Count > 2)
            {
                _mainVm.SelectedItem.Shapes.Remove(_mainVm.SelectedItem.Shapes.First(x => x.Id == id));
            }
            _mainVm.SelectedItem.ReNumberShapes();
            Item.Calculate(_mainVm.SelectedItem);
            _mainVm.ThisIsBestPractice();
            _mainVm.SelectedItem = _mainVm.SelectedItem;
        }

        public event EventHandler CanExecuteChanged;
    }
}