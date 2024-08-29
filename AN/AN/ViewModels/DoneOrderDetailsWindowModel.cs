using AN.Dialogs;
using AN.Models;
using AN.Services;
using MVVMTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.ViewModels
{
    public class DoneOrderDetailsWindowModel : BaseViewModel, IDialogRequestClose
    {
        public IMessanger Messanger { get; set; }
        public IDialogService DialogService { get; set; }
        public Order Order { get; set; }
        public string WorkerFullName { get; set; }

        public decimal GoldInOrder { get; set; }

        public DoneOrderDetailsWindowModel(IMessanger messanger, IDialogService dialogService)
        {
            this.Messanger = messanger;
            this.DialogService = dialogService;
        }

        public DoneOrderDetailsWindowModel(IMessanger messanger, IDialogService dialogService,string workerFullName, Order order)
        {
            this.Messanger = messanger;
            this.DialogService = dialogService;
            this.WorkerFullName = workerFullName;
            this.Order = order;
            this.GoldInOrder = Order.GoldWeight - Order.Waste;
        }
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

    }
}
