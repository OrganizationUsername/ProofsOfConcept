using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using LuggerWPF;

namespace Commands.LuggerWPF
{
    public class AddShapeCommand : ICommand
    {
        private readonly MainVm _mainVm;

        public AddShapeCommand(MainVm mainVm)
        {
            _mainVm = mainVm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
            throw new NotImplementedException();
        }

        public void Execute(object circleRadioButton)
        {
            if (circleRadioButton is RadioButton radiobutton)
            {
                if (radiobutton.IsChecked != null && (bool)radiobutton.IsChecked)
                {
                    Circle newCircle = new Circle()
                    {
                        Owner = _mainVm.SelectedItem,
                        Diameter = _mainVm.UnsavedCircle.Diameter,
                        X = _mainVm.UnsavedCircle.X,
                        Y = _mainVm.UnsavedCircle.Y
                    };
                    var aRectangle = _mainVm.SelectedItem.GetRectangle();
                    if (aRectangle is null) { return; }

                    if (!Item.CircleCenterOutsideRectangle(newCircle, aRectangle))
                    {
                        _mainVm.SelectedItem.Shapes.Add(newCircle);
                    }
                }
                else
                {
                    _mainVm.SelectedItem.Shapes.Add(
                        new Rectangle()
                        {
                            Owner = _mainVm.SelectedItem,
                            Height = _mainVm.UnsavedRectangle.Height,
                            Width = _mainVm.UnsavedRectangle.Width,
                            X = _mainVm.UnsavedRectangle.X,
                            Y = _mainVm.UnsavedRectangle.Y
                        }
                    );
                }
                _mainVm.RecalculateAllRatios();
                _mainVm.DrawSomethingInVmBecauseIDontKnowBetter();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}