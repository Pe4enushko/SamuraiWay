using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SamuraiWay.Views
{
    /// <summary>
    /// Логика взаимодействия для ChalPage.xaml
    /// </summary>
    public partial class ChalPage : Page
    {
        public ChalPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.showUpdate && Services.CheckThings.CheckUpdates())
            {
                new UpdateWindow().Show();
            }
        }
    }
}
