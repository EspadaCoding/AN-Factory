using AN.Dialogs;
using AN.Message;
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
    public class OrderDetailsWindowModel : BaseViewModel, IDialogRequestClose
    {
        public IMessanger Messanger { get; set; }
        public Order SelectedOrder { get; set; }

        public IDialogService DialogService { get; set; }

        private string workerFullName;
        public string WorkerFullName
        {
            get => workerFullName;
            set
            {
                ChangeProp(out workerFullName, value);
            }
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public OrderDetailsWindowModel(IMessanger messanger, IDialogService dialogService)
        {
            this.Messanger = messanger;
            this.DialogService = dialogService;
        }

        public OrderDetailsWindowModel(IMessanger messanger, IDialogService dialogService, string workerFullName, Order order)
        {
            this.Messanger = messanger;
            this.SelectedOrder = order;
            this.DialogService = dialogService;
            this.workerFullName = workerFullName;
        }

        
    }
}
