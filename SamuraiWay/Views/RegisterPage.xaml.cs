using SamuraiWay.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SamuraiWay.Views
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RegisterViewModel vm = this.DataContext as RegisterViewModel;
            vm.Pass = Txt_Password.Password;
        }
    }
}
