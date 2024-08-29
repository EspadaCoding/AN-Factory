using AN.Models;
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

namespace AN.Views
{
    /// <summary>
    /// Логика взаимодействия для WorkersDetailsView.xaml
    /// </summary>
    /// 
    
    public partial class WorkersDetailsView : UserControl
    {
        public WorkersDetailsView()
        {
            InitializeComponent();
            
        }
        private void Refresh_DataGrid(object sender, RoutedEventArgs e)
        {
            Orders.Items.Refresh();
        }

        private void Orders_MouseMove(object sender, MouseEventArgs e)
        {
            Orders.Items.Refresh();
        }
    }
}
