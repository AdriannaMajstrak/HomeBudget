using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeBudget.Client.Utilities
{
    public class RelayCommand<T> : ICommand
    {
        Action<T> method;

        public RelayCommand(Action<T> method)
        {
            this.method = method;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter != null)
            {
                method((T)parameter);
            }
        }
    }

    public class RelayCommand : ICommand
    {   
        Action methodNoParam;
        
        public RelayCommand(Action method)
        {
            this.methodNoParam = method;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            methodNoParam();
        }
    }

  
}
