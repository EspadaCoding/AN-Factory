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
    /// Логика взаимодействия для PrintModelView.xaml
    /// </summary>
    public partial class PrintModelView : Page
    {
        public PrintModelView(WatchModel watchModel)
        {
            InitializeComponent();
            ModelName.Content += watchModel.Name;
            watchModel.Details.Add(new("26", "общая сумма",0, watchModel.TotalPrice));
            ModelDetails.ItemsSource = watchModel.Details;
            Date.Content = DateTime.Now.Day.ToString() + '.' + DateTime.Now.Month.ToString() + '.' + DateTime.Now.Year.ToString();
        }

        public PrintModelView(WatchModel watchModel, DateTime date)
        {
            InitializeComponent();
            ModelName.Content += watchModel.Name;
            watchModel.Details.Add(new("26", "общая сумма", 0, watchModel.TotalPrice));
            ModelDetails.ItemsSource = watchModel.Details;
            Date.Content = date.Day.ToString() + '.' + date.Month.ToString() +'.' + date.Year.ToString();
        }
    }
}
