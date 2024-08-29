using AN.Dialogs;
using AN.Message;
using AN.Models;
using AN.Repository;
using AN.Services;
using MVVMTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AN.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        public IMessanger Messanger { get; set; }
        public IWorkerRepository WorkerRepository { get; set; }
        public ICollectionView WorkerOrdercollectionView { get; }
        private ObservableCollection<Order> _orders = new();
        private readonly IDialogService dialogService;
        public IDialogService DialogService => dialogService;

        private string _OrderModelFilter = string.Empty;
        private string _OrderPartiaFilter = string.Empty;
        public string OrderModelFilter
        {
            get => _OrderModelFilter;
            set
            {

                ChangeProp(out _OrderModelFilter, value);
                WorkerOrdercollectionView.Refresh();
            }
        }
        public string OrderPartiaFilter
        {
            get => _OrderPartiaFilter;
            set
            {

                ChangeProp(out _OrderPartiaFilter, value);
                WorkerOrdercollectionView.Refresh();
            }
        }

        public ObservableCollection<Order> BubbleSort(ObservableCollection<Order> a)
        {
            var n = a.Count;
            bool swapRequired;
            for (int i = 0; i < n - 1; i++)
            {
                swapRequired = false;
                for (int j = 0; j < n - i - 1; j++)
                    if (int.Parse(a[j].Batch_number) > int.Parse(a[j + 1].Batch_number))
                    {
                        var tempVar = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = tempVar;
                        swapRequired = true;
                    }
                if (swapRequired == false)
                    break;
            }
            return a;
        }



        public OrdersViewModel(IMessanger messanger, IWorkerRepository workerRepository, IDialogService dialogService)
        {
            this.Messanger = messanger;
            this.WorkerRepository = workerRepository;
            WorkerOrdercollectionView = CollectionViewSource.GetDefaultView(_orders);
            WorkerOrdercollectionView.Filter = FilterOrders;
            this.dialogService = dialogService;
            Messanger.Subcribe<RefreshOrdersMessage>(n => {
                _orders.Clear();
                List<Order> ListOrders = new List<Order>();
                for (int i = 0; i < workerRepository.GetAllWorkers().Count; i++)
                {
                    for (int m = 0; m < workerRepository.GetAllWorkers()[i].Orders.Count; m++)
                    {
                        if (workerRepository.GetAllWorkers()[i].Orders[m].Model != "1")
                        {
                            if (workerRepository.GetAllWorkers()[i].Orders[m].IsDone)
                            {
                                ListOrders.Add((Order)workerRepository.GetAllWorkers()[i].Orders[m].Clone());
                            }
                        }
                    }
                }

                ListOrders.Sort((order1, order2) =>
                {
                    int n1 = int.Parse(order1.Batch_number);
                    int n2 = int.Parse(order2.Batch_number);
                    return n1.CompareTo(n2);
                }
                );

                for(int i = 0; i < ListOrders.Count;i++)
                {
                    _orders.Add(ListOrders[i]);
                }

                try
                {
                    for (int i = 0; i < _orders.Count; i++)
                    {
                        for (int x = 0; x < _orders.Count; x++)
                        {
                            if (_orders[i] != _orders[x])
                            {
                                if (_orders[i].Model.Trim() == _orders[x].Model.Trim()
                                        && _orders[i].Batch_number.Trim() == _orders[x].Batch_number.Trim())
                                {
                                    _orders[i].Number += _orders[x].Number;
                                    _orders.RemoveAt(x);

                                }
                            }
                        }
                    }
                }
                catch(Exception ex)
                {

                }
            });

        }

        private Command navBack;
        public Command NavBack => navBack ??= new Command(() => { Messanger.Send<NavigationMessage>(new NavigationMessage { ViewModelType = typeof(MenuViewModel) }); });

        

        private bool FilterOrders(object obj)
        {

            int b = 0 ;
            if (obj is Order order)
            {
                if(OrderModelFilter == "" && OrderPartiaFilter == "")
                {
                    return true;
                }
                else if (OrderModelFilter == "")
                {
                    return order.Batch_number == OrderPartiaFilter;
                }
                else if (OrderPartiaFilter == "")
                {
                    return order.Model == OrderModelFilter;
                }
                else
                {
                    if (order.Model == OrderModelFilter)
                    {

                        return order.Batch_number == OrderPartiaFilter;
                    }
                    else if (order.Batch_number == OrderPartiaFilter)
                    {

                        return order.Model == OrderModelFilter; 
                    }
                }
            }
            return false;
        }

        private void NavPartia()
        {
            var viewModel = new PartianNumberWindowModel(Messanger, DialogService, _orders);

            bool? result = DialogService.ShowDialog(viewModel);

            if (result.HasValue)
            {
                if (result.Value)
                {
                    // Accepted
                }
                else
                {
                    // Cancelled
                }
            }
        }
        private Command navPartiaCommand;
        public Command NavPartiaCommand => navPartiaCommand ??= new Command(() => { NavPartia(); });
    }
}
