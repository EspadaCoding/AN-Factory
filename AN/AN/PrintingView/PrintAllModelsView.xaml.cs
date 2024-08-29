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
    /// Логика взаимодействия для PrintAllModelsView.xaml
    /// </summary>
    public partial class PrintAllModelsView : Page
    {
        public PrintAllModelsView(List<WatchModel> models, int pagenumber)
        {
            InitializeComponent();

            Models.ItemsSource = models;
            PageNumber.Content = pagenumber;
            DateTime date = DateTime.Now;
            Date.Content = date.Day.ToString() + '.' + date.Month.ToString() + '.' + date.Year.ToString();
        }
    }
}
