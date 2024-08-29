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

namespace AN.PrintingView
{
    /// <summary>
    /// Логика взаимодействия для PrintOrderView.xaml
    /// </summary>
    public partial class PrintOrderView : Page
    {
        public PrintOrderView(Worker worker, Order order)
        {
            InitializeComponent();
            WorkerName.Content = worker.FirstName + ' ' + worker.LastName;
            Model.Content += order.Model;
            BanchNumber.Content += order.Batch_number;
            Number.Content += order.Number.ToString();
            GoldWeight.Content += order.GoldWeight.ToString();
            BronzeWeight.Content += order.BronzeWeight.ToString();
            Date.Content = order.SendingDate.Day.ToString() + "." + order.SendingDate.Month.ToString() + "." + order.SendingDate.Year.ToString();
            
        }
    }
}
