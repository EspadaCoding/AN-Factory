using AN.Dialogs;
using AN.Message;
using AN.Models;
using AN.Repository;
using AN.Services;
using MVVMTools;
using System;

namespace AN.ViewModels
{
    public class AddOrderWindowModel : BaseViewModel, IDialogRequestClose
    {
        public IMessanger Messanger { get; set; }
        public IDialogService DialogService { get; set; }
        public AddOrderWindowModel(IMessanger messanger, IDialogService dialogService)
        {
            this.Messanger = messanger;
            this.DialogService = dialogService;
        }

        public AddOrderWindowModel(IMessanger messanger, Worker worker, IDialogService dialogService)
        {
            this.Messanger = messanger;
            this.DialogService = dialogService;
            this.SelectedWorker = worker;
            this.workerFullName = worker.FirstName + ' ' + worker.LastName;
        }
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;


        private Worker selectedWorker;
        public Worker SelectedWorker
        {
            get => selectedWorker;
            set {
                ChangeProp(out selectedWorker, value);
            }
        }

        private string workerFullName;
        public string WorkerFullName
        {
            get => workerFullName;
            set
            {
                ChangeProp(out workerFullName, value);
            }
        }
        //////////////////Поля///////////////////////
        private string editorModel;
        public string EditorModel
        {
            get => editorModel;
            set {
                ChangeProp(out editorModel, value);
            }
        }

        private string editorPartiaNumber;
        public string EditorPartiaNumber
        {
            get => editorPartiaNumber;
            set {
                ChangeProp(out editorPartiaNumber, value);
            }
        }

        private int editorNumber;
        public int EditorNumber
        {
            get => editorNumber;
            set {
                ChangeProp(out editorNumber, value);
                EditorTotalMoney = editorNumber * editorCostOne;
            }
        }

        private decimal editorCostOne;
        public decimal EditorCostOne
        {
            get => editorCostOne;
            set {
                if (editorCostOne != value)
                {
                    editorCostOne = value;
                    ChangeProp(out editorCostOne, value);
                    EditorTotalMoney = editorNumber * editorCostOne;
                }
            }
        }

        private decimal editorTotalMoney;
        public decimal EditorTotalMoney
        {
            get => editorTotalMoney;
            set {
                if (editorTotalMoney != value)
                {
                    editorTotalMoney = value;
                    ChangeProp(out editorTotalMoney, value);
                }
            }
        }

        private Karat editorKarat;
        public Karat EditorKarat
        {
            get => editorKarat;
            set {
                ChangeProp(out editorKarat, value);
            }
        }

        //private decimal editorGoldPrice;
        //public decimal EditorGoldPrice
        //{
        //    get => editorGoldPrice;
        //    set {
        //        ChangeProp(out editorGoldPrice, value);
        //    }
        //}

        private decimal editorGoldWeight;
        public decimal EditorGoldWeight
        {
            get => editorGoldWeight;
            set {
                if (editorGoldWeight != value)
                {
                    editorGoldWeight = value;
                    ChangeProp(out editorGoldWeight, value);
                }
            }
        }

        private decimal editorBronzeWeight;
        public decimal EditorBronzeWeight
        {
            get => editorBronzeWeight;
            set {
                if (editorBronzeWeight != value)
                {
                    editorBronzeWeight = value;
                    ChangeProp(out editorBronzeWeight, value);
                }

            }
        }

        //////////////////Поля///////////////////////

        private Command addOrderCommand;
        public Command AddOrderCommand => addOrderCommand ??= new Command(AddOrder);
        private void AddOrder()
        {
            Order newOrder = new(editorPartiaNumber, editorModel.Trim(), editorNumber, editorKarat, editorBronzeWeight, editorGoldWeight);

            IWorkerRepository workerRepository = new WorkerRepository();
            workerRepository.RemoveWorker(SelectedWorker);
            SelectedWorker.Orders.Add(newOrder);
            workerRepository.AddWorker(SelectedWorker);
            Messanger.Send<NavigationMessage>(new NavigationMessage { ViewModelType = typeof(WorkerRepository) });

            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
            

            Clear();
        }


        private void Clear()
        {

            this.EditorModel = "";

            this.EditorPartiaNumber = "";

            this.EditorNumber = 0;

            this.EditorCostOne = 0;

            this.EditorKarat = 0;

            //this.EditorGoldPrice = "";

            this.EditorGoldWeight = 0;

            this.EditorBronzeWeight = 0;

        }

    }
}
