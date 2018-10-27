using System;
using System.Timers;
using System.Windows.Input;

namespace GreenLeaf.Core.Common
{
    class Command : ICommand
    {
        private readonly Action<object> _methodToExecute = null;
        private readonly Func<bool> _methodToDetectCanExecute = null;

        public Command(Action<object> methodToExecute, Func<bool> methodToDetectCanExecute)
        {
            Timer timer = new Timer
            {
                Interval = 500
            };
            timer.Elapsed += CanExecuteChangedEventTimer_Tick;

            _methodToDetectCanExecute = methodToDetectCanExecute;
            _methodToExecute = methodToExecute;

            timer.Start();
        }

        public void Execute(object parameter)
        {
            _methodToExecute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (_methodToDetectCanExecute != null)
            {
                return _methodToDetectCanExecute();
            }

            return false;
        }

        public event EventHandler CanExecuteChanged;

        void CanExecuteChangedEventTimer_Tick(object sender, ElapsedEventArgs e)
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
