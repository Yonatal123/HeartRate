using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HeartRate
{
    public class RelayCommand : ICommand
    {
        public RelayCommand(Predicate<object> p_canExecute, Action<object> p_execute)
        {
            m_canExecute = p_canExecute;
            m_execute = p_execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object p_parameter)
        {
            return m_canExecute(p_parameter);
        }

        public void Execute(object p_parameter)
        {
            m_execute(p_parameter);
        }


        private Predicate<object> m_canExecute;
        private Action<object> m_execute;
    }
}
