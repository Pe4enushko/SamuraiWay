using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;

namespace SamuraiWay.Views
{
    /// <summary>
    /// Логика взаимодействия для UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        public UpdateWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if((bool)Chb_DontShow.IsChecked)
            {
                Properties.Settings.Default.showUpdate = false;
                Properties.Settings.Default.Save();
            }
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/Pe4enushko/SamuraiWay/releases") { UseShellExecute = true});
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Txt_Update.Blocks.Add(new Paragraph(new Run("New version is " + new SamuraiDbWork.GlobalDB.Checks().CheckVersion().ToString(3))));
        }
    }
}
