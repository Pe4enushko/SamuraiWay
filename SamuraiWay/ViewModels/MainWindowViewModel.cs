using SamuraiWay.Views;
using System;
using System.Windows.Controls;

namespace SamuraiWay.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        Page activePage;

        public Page ActivePage { get => activePage; set { activePage = value; OnPropertyChanged(); } }
        public MainWindowViewModel()
        {
            if (Properties.Settings.Default.isLogged)
            {
                ActivePage = new ChalPage();
            }
            else
            {
                ActivePage = new RegisterPage();
                var vm = ((RegisterPage)ActivePage).DataContext as RegisterViewModel;
                vm.AuthSuccessEvent += Vm_AuthSuccessEvent;
            }
        }

        private void Vm_AuthSuccessEvent(object sender, EventArgs e)
        {
            Properties.Settings.Default.isLogged = true;
            Properties.Settings.Default.Save();
            ActivePage = new ChalPage();
        }
    }
}
