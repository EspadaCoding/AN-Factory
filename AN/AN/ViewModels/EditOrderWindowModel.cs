using AN.Dialogs;
using AN.Models;
using AN.Repository;
using AN.Services;
using MVVMTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.ViewModels
{
    public class EditOrderWindowModel : BaseViewModel, IDialogRequestClose
    {
        public IMessanger Messanger { get; set; }
        public IDialogService DialogService { get; set; }
        public IWorkerRepository WorkerRepository { get; set; }
        public Order Order { get; set; }
        public Order CloneOrder { get; set; }
        public Worker Worker { get; set; }
        public string WorkerFullName { get; set; }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public EditOrderWindowModel(IMessanger messanger, IDialogService dialogService,IWorkerRepository workerRepository)
        {
            this.Messanger = messanger;
            this.DialogService = dialogService;
            this.WorkerRepository = workerRepository;
        }

        public EditOrderWindowModel(IMessanger messanger, IDialogService dialogService, Worker worker, Order order)
        {
            this.Messanger = messanger;
            this.DialogService = dialogService;
            this.Order = order;
            this.CloneOrder = order.Clone() as Order;
            this.Worker = worker;
            this.WorkerFullName = worker.FirstName + ' ' + worker.LastName;
        }

        private Command cancerEditCommand;
        public Command CancerEditCommand => cancerEditCommand ??= new Command(() => { Cancer(); });

        private Command saveEditCommand;
        public Command SaveEditCommand => saveEditCommand ??= new Command(() => { SaveChanges(); });

        public void Cancer()
        {
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        public void SaveChanges()
        {

            WorkerRepository workerRepository = new();
            var Workers = workerRepository.GetAllWorkers();
            int WorkerIndex = 0;
            for(int i = 0; i < Workers.Count;i++)
            {
                if(Worker.FirstName == Workers[i].FirstName)
                {
                    WorkerIndex = i;
                    break;
                }
            }

            int OrderIndex = Workers[WorkerIndex].GetOrderID(CloneOrder);

            if (OrderIndex != -1)
            {

                workerRepository.RemoveWorker(Workers[WorkerIndex]);
                Order.Model = Order.Model.Trim();
                Order.Batch_number = Order.Batch_number.Trim();
                Worker.Orders[OrderIndex] = Order;
                workerRepository.AddWorker(Worker);
            }

            

            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));

        }
    }
}

