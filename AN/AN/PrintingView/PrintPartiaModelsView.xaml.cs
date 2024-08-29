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
    public class ModelNumber
    {
        public ModelNumber( string model,int number)
        {
            Model = model;
            Number = number;
           
        }
        public string Model { get; set; }
        public int Number { get; set; }
        
    }
    public partial class PrintPartiaModelsView : Page
    {
        public List<Order> Orders { get; set; }

        public PrintPartiaModelsView(List<Order> orders,int page, bool isLast, int totalnumber)
        {
            List<ModelNumber> ModelNumbers = new();

            
            
            InitializeComponent();
            PartiaName.Content += orders[0].Batch_number;
            for (int i = 0; i < orders.Count;i++)
            {
                ModelNumbers.Add(new ModelNumber( orders[i].Model, orders[i].Number));
                
            }

            PageNumber.Content = page;
            Models.ItemsSource = ModelNumbers;
            DateTime date = DateTime.Now;
            Date.Content = date.Day.ToString() + '.' + date.Month.ToString() + '.' + date.Year.ToString();
            if (isLast)
            {
                
                TotalNumber.Content += totalnumber.ToString();
                
                TotalNumber.Visibility = Visibility.Visible;
            }
            else
            {
                TotalNumber.Visibility = Visibility.Hidden;
            }
        }
    }
}
