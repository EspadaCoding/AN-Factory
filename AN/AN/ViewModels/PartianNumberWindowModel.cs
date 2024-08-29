using AN.Dialogs;
using AN.Models;
using AN.PrintingView;
using AN.Repository;
using AN.Services;
using MVVMTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.ViewModels
{
    public class PartianNumberWindowModel : BaseViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public IMessanger Messanger { get; set; }
        public IDialogService DialogService { get; set; }
        public IWorkerRepository WorkerRepository { get; set; }

        public ObservableCollection<Order> Orders { get; set; }

        public PartianNumberWindowModel(IMessanger messanger, IDialogService dialogService, IWorkerRepository workerRepository)
        {
            Messanger = messanger;
            DialogService = dialogService;
            WorkerRepository = workerRepository;
        }

        public PartianNumberWindowModel(IMessanger messanger, IDialogService dialogService, ObservableCollection<Order> orders)
        {
            Messanger = messanger;
            DialogService = dialogService;
            Orders = orders;
        }

        private string partia;
        public string Partia
        {
            get => partia;
            set
            {
                ChangeProp(out partia, value);
            }
        }

        private void PrintPartia()
        {
            List<Order> onePartiaOrders = new List<Order>();
            int ordersnumber = 0;
            for (int i = 0; i < Orders.Count;i++ )
            {
                if(Orders[i].Batch_number.Trim() == Partia)
                {
                    onePartiaOrders.Add(Orders[i]);
                }
            }

            for (int i = 0; i < onePartiaOrders.Count; i++)
            {
                ordersnumber += onePartiaOrders[i].Number;
            }

            int reminder = onePartiaOrders.Count % 26;
            int divider = onePartiaOrders.Count / 26;

            List<List<Order>> splitedOrders = new List<List<Order>>();
            int b = 0;
            for(int i = 0; i < divider;i++)
            {
                splitedOrders.Add(new List<Order>());
                for (int n = 0; n < 26; n++, b++)
                {
                    splitedOrders[i].Add(onePartiaOrders[b]);

                }
            }

            if (reminder != 0)
            {

                splitedOrders.Add(new List<Order>());

                for (int i = 0, m = onePartiaOrders.Count - reminder; i < reminder; i++, m++)
                {
                    splitedOrders[^1].Add(onePartiaOrders[m]);

                }
            }

            System.Windows.Controls.PrintDialog printDialog = new();
            PrintPartiaModelsView printOrderView;
            if (printDialog.ShowDialog() == true)
            {
                for (int i = 0; i < divider; i++)
                {
                    printOrderView = new(splitedOrders[i], i + 1, false, 0);
                    printDialog.PrintVisual(printOrderView, "Invoice");
                }
                if (reminder != 0)
                {
                    printOrderView = new(splitedOrders[^1], divider + 1, true, ordersnumber);
                    printDialog.PrintVisual(printOrderView, "Invoice");
                }
            }



            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        private Command printPartiaCommand;
        public Command PrintPartiaCommand => printPartiaCommand ??= new Command(() => { PrintPartia(); });
    }
}
