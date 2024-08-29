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
    public class AcceptingWorkWindowModel : BaseViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public IMessanger Messanger { get; set; }
        public IDialogService DialogService { get; set; }
        public Order Order { get; set; }
        public Worker Worker { get; set; }
        public string WorkerFullName { get; set; }

        public AcceptingWorkWindowModel(IMessanger messanger, IDialogService dialogService)
        {
            this.Messanger = messanger;
            this.DialogService = dialogService;
        }

        public AcceptingWorkWindowModel(IMessanger messanger, IDialogService dialogService,Worker worker, Order order)
        {
            this.Messanger = messanger;
            this.DialogService = dialogService;
            this.Order = order;
            this.Worker = worker;
            this.WorkerFullName = worker.FirstName + ' ' + worker.LastName;
        }

        //////////Поля заполения//////////
        private decimal editorTotalWeight;
        public decimal EditorTotalWeight
        {
            get => editorTotalWeight;
            set
            {
                if (editorTotalWeight != value)
                {
                    editorTotalWeight = value;
                    Order.TotalWeight = editorTotalWeight;
                    TxtBlockGoldWeight = Order.TotalWeight - Order.BronzeWeight;
                    TxtBlockWaste = Order.GoldWeight - txtBlockGoldWeight;
                    ChangeProp(out editorTotalWeight, value);
                }
            }
        }
        private decimal txtBlockGoldWeight;
        public decimal TxtBlockGoldWeight
        {
            get => txtBlockGoldWeight;
            set
            {
                if (txtBlockGoldWeight != value)
                {
                    txtBlockGoldWeight = value;
                    ChangeProp(out txtBlockGoldWeight, value);
                }
            }
        }
        

        private decimal editorGoldCost;
        public decimal EditorGoldCost
        {
            get => editorGoldCost;
            set
            {
                if (editorGoldCost != value)
                {
                    editorGoldCost = value;
                    ChangeProp(out editorGoldCost, value);
                }
            }
        }

        
        public decimal txtBlockWaste;
        public decimal TxtBlockWaste
        {
            get => txtBlockWaste;
            set
            {
                if (txtBlockWaste != value)
                {
                    txtBlockWaste = value;
                    ChangeProp(out txtBlockWaste, value);
                }
            }
        }
        //////////Поля заполения//////////

        private Command cancerEditCommand;
        public Command CancerEditCommand => cancerEditCommand ??= new Command(() => { Cancer(); });

        private Command saveEditCommand;
        public Command SaveEditCommand => saveEditCommand ??= new Command(() => { SaveChanges(); });

        public void Cancer()
        {
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        public int SaveChanges()
        {
            WorkerRepository workerRepository = new();
            var Workers = workerRepository.GetAllWorkers();
            int WorkerIndex = 0;
            for (int i = 0; i < Workers.Count; i++)
            {
                if (Worker.FirstName == Workers[i].FirstName)
                {
                    WorkerIndex = i;
                    break;
                }
            }
            var CloneOrder = Order.Clone() as Order;

            int OrderIndex = Workers[WorkerIndex].GetOrderID(CloneOrder);

            if (OrderIndex != -1)
            {
                workerRepository.RemoveWorker(Workers[WorkerIndex]);
                Worker.Orders[OrderIndex].Waste = txtBlockWaste;
                Worker.Orders[OrderIndex].TotalWeight = editorTotalWeight;
                Worker.Orders[OrderIndex].Gold_cost = editorGoldCost;
                Worker.Orders[OrderIndex].AcceptingDate = DateTime.Now;
                Worker.Orders[OrderIndex].OrderDone();
                workerRepository.AddWorker(Worker);
            }
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
            return 0;
        }
    }
}
