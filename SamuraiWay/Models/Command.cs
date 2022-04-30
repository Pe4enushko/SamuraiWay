using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SamuraiWay.Models
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        Action act;
        public Command(Action action)
        {
            this.act = action;
        }
        public bool CanExecute(object parameter)
        {
            return act != null;
        }

        public void Execute(object parameter)
        {
            act.Invoke();
        }
    }
}
