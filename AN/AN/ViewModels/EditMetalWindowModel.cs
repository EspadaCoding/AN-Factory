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
using System.Windows;

namespace AN.ViewModels
{
    public class EditMetalWindowModel : BaseViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public IMessanger Messanger { get; set; }
        public IDialogService DialogService { get; set; }
        public IWorkerRepository WorkerRepository { get; set; }
        

        public string MetalText { get; set; }
        private bool IsGold { get; set; }
        public Order Order { get; set; }
        public Worker Worker { get; set; }

        private double changing;
        public double Changing
        {
            get => changing;
            set {
                if (changing != value)
                {
                    changing = value;
                    ChangeProp(out changing, value);
                }
            }
        }

        public EditMetalWindowModel(IMessanger messanger, IDialogService dialogService, IWorkerRepository workerRepository)
        {
            this.Messanger = messanger;
            this.DialogService = dialogService;
            this.WorkerRepository = workerRepository;
        }

        public EditMetalWindowModel(IMessanger messanger, IDialogService dialogService,Worker worker, Order order, bool isGold)
        {
            this.Messanger = messanger;
            this.DialogService = dialogService;
            this.IsGold = isGold;
            this.Order = order;
            this.Worker = worker;
            this.changing = 0;
            if(IsGold)
            {
                this.MetalText = "Qızıl";
            }
            else
            {
                this.MetalText = "Korpus";
            }
        }

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
            if (changing != 0)
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

                int OrderIndex = Workers[WorkerIndex].GetOrderID(Order);

                if (OrderIndex != -1)
                { 

                    workerRepository.RemoveWorker(Workers[WorkerIndex]);

                    if (IsGold)
                    {
                        if (Worker.Orders[OrderIndex].GoldWeight + Convert.ToDecimal(changing) < 0)
                        {
                            MessageBox.Show("Weight can't be less than 0", "Error", MessageBoxButton.OK);
                            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
                            return 1;

                        }
                        if (changing > 0)
                        {
                            Worker.Orders[OrderIndex].GoldHistory += $"+{changing}";
                        }
                        else
                        {
                            Worker.Orders[OrderIndex].GoldHistory += $"{changing}";
                        }
                        Worker.Orders[OrderIndex].GoldWeight += Convert.ToDecimal(changing);
                    }
                    else
                    {
                        if (Worker.Orders[OrderIndex].BronzeWeight + Convert.ToDecimal(changing) < 0)
                        {
                            MessageBox.Show("Weight can't be less than 0", "Error", MessageBoxButton.OK);
                            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
                            return 1;
                        }

                        if (changing > 0)
                        {
                            Worker.Orders[OrderIndex].BronzeHistory += $"+{changing}";
                        }
                        else
                        {
                            Worker.Orders[OrderIndex].BronzeHistory += $"{changing}";
                        }
                        Worker.Orders[OrderIndex].BronzeWeight += Convert.ToDecimal(changing);
                    }

                    workerRepository.AddWorker(Worker);
                }
            }


            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
            return 0;
        }
    }
}
