using AN.Dialogs;
using AN.Message;
using AN.Models;
using AN.Repository;
using AN.Services;
using AN.Views;
using MVVMTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using AN.PrintingView;

namespace AN.ViewModels
{
    public class WorkersDetailsViewModel : BaseViewModel
    {
        public IMessanger Messanger { get; set; }
        private readonly IDialogService dialogService;
        public ICollectionView WorkerOrdercollectionView { get; }
        private readonly ObservableCollection<Order> _order = new();

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


        public WorkersDetailsViewModel(IMessanger messanger, IDialogService dialogService)
        {

            this.Messanger = messanger;
            this.dialogService = dialogService;
            NavAddOrderCommand = new ActionCommand(p => NavAddOrder());
            NavOrderDetailsCommand = new ActionCommand(p => NavOrderDetails());
            NavDoneOrderDetailsCommand = new ActionCommand(p => NavDoneOrderDetails());
            NavEditOrderCommand = new ActionCommand(p => NavEditOrder());
            NavEditGoldCommand = new ActionCommand(p => NavEditGold());
            NavEditBronzeCommand = new ActionCommand(p => NavEditBronze());
            NavAcceptingWorkCommand = new ActionCommand(p => NavAcceptingWork());
            DisablleWorkCommand = new ActionCommand(p => DisablleWork());
            this.SelectedOrder = new Order();

            messanger.Subcribe<OrderDetailMessage>(i =>
            {
                WorkerOrdercollectionView.Refresh();
            });
        }

        public ICommand NavAddOrderCommand { get; }
        public ICommand NavOrderDetailsCommand { get; }
        public ICommand NavDoneOrderDetailsCommand { get; }
        public ICommand NavEditOrderCommand { get; }
        public ICommand NavEditGoldCommand { get; }
        public ICommand NavEditBronzeCommand { get; }
        public ICommand NavAcceptingWorkCommand { get; }
        public ICommand DisablleWorkCommand { get; }


        private void NavAddOrder()
        {
            var viewModel = new AddOrderWindowModel(Messanger, selectedWorker, DialogService);

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
            _order.Clear();
            foreach (var item in SelectedWorker.Orders) { _order.Add(item); }
        }
        private void NavOrderDetails()
        {
            var viewModel = new OrderDetailsWindowModel(Messanger, dialogService, selectedWorker.FirstName + ' ' + selectedWorker.LastName, selectedOrder);

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
            _order.Clear();
            foreach (var item in SelectedWorker.Orders) { _order.Add(item); }
        }
        private void NavDoneOrderDetails()
        {
            var viewModel = new DoneOrderDetailsWindowModel(Messanger, dialogService, selectedWorker.FirstName + ' ' + selectedWorker.LastName, selectedOrder);

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
            _order.Clear();
            foreach (var item in SelectedWorker.Orders) { _order.Add(item); }
        }
        private void NavEditOrder()
        {
            var viewModel = new EditOrderWindowModel(Messanger, dialogService, SelectedWorker, selectedOrder);

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
            _order.Clear();
            foreach (var item in SelectedWorker.Orders) { _order.Add(item); }
        }
        private void NavEditGold()
        {
            var viewModel = new EditMetalWindowModel(Messanger, dialogService, SelectedWorker, selectedOrder, true);

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
            _order.Clear();
            foreach (var item in SelectedWorker.Orders) { _order.Add(item); }
        }
        private void NavEditBronze()
        {
            var viewModel = new EditMetalWindowModel(Messanger, dialogService, SelectedWorker, selectedOrder, false);

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
            _order.Clear();
            foreach (var item in SelectedWorker.Orders) { _order.Add(item); }
        }
        private void NavAcceptingWork()
        {
            var viewModel = new AcceptingWorkWindowModel(Messanger, DialogService, selectedWorker, selectedOrder);

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
            _order.Clear();
            foreach (var item in SelectedWorker.Orders) { _order.Add(item); }
        }

        public WorkersDetailsViewModel(IMessanger messanger, Worker worker, IDialogService dialogService)
        {
            WorkerOrdercollectionView = CollectionViewSource.GetDefaultView(_order);
            WorkerOrdercollectionView.Filter = FilterOrders;
            this.Messanger = messanger;
            SelectedWorker = worker;
            foreach (var item in SelectedWorker.Orders) { _order.Add(item); }
            this.dialogService = dialogService;
            NavAddOrderCommand = new ActionCommand(p => NavAddOrder());
            NavOrderDetailsCommand = new ActionCommand(p => NavOrderDetails());
            NavDoneOrderDetailsCommand = new ActionCommand(p => NavDoneOrderDetails());
            NavEditOrderCommand = new ActionCommand(p => NavEditOrder());
            NavEditGoldCommand = new ActionCommand(p => NavEditGold());
            NavEditBronzeCommand = new ActionCommand(p => NavEditBronze());
            NavAcceptingWorkCommand = new ActionCommand(p => NavAcceptingWork());
            DisablleWorkCommand = new ActionCommand(p => DisablleWork());
            this.SelectedOrder = new Order();

        }
        private bool FilterOrders(object obj)
        {
            int b = 0;
            if (obj is Order order)
            {
                if (OrderModelFilter == "" && OrderPartiaFilter == "")
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

        private Worker selectedWorker;
        public Worker SelectedWorker
        {
            get => selectedWorker;
            set => ChangeProp(out selectedWorker, value);
        }


        private Order selectedOrder;
        public Order SelectedOrder
        {
            get => selectedOrder;
            set => ChangeProp(out selectedOrder, value);
        }


        // Команда для удаления заказа из списка
        private Command delCommand;
        public Command DelCommand => delCommand ??= new Command(() => { Del(); });

        private Command printOrderCommand;
        public Command PrintOrderCommand => printOrderCommand ??= new Command(() => { PrintOrder(); });

        private Command printDoneOrderCommand;
        public Command PrintDoneOrderCommand => printDoneOrderCommand ??= new Command(() => { PrintDoneOrder(); });


        public IDialogService DialogService => dialogService;

        private void Del()
        {
            WorkerRepository workerRepository = new();
            workerRepository.RemoveWorker(SelectedWorker);
            SelectedWorker.Orders.Remove(SelectedOrder);
            workerRepository.AddWorker(SelectedWorker);
            _order.Remove(SelectedOrder);

        }
        private void PrintOrder()
        {
            PrintOrderView printOrderView = new(selectedWorker,selectedOrder);
            System.Windows.Controls.PrintDialog printDialog = new();
            if(printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(printOrderView, "Invoice");
            }
        }


        private void PrintDoneOrder()
        {
            PrintDoneOrderView printOrderView = new(selectedWorker, selectedOrder);
            System.Windows.Controls.PrintDialog printDialog = new();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(printOrderView, "Invoice");
            }

        }
        private void DisablleWork()
        {
            WorkerRepository workerRepository = new();
            var Workers = workerRepository.GetAllWorkers();
            int WorkerIndex = 0;
            for (int i = 0; i < Workers.Count; i++)
            {
                if (SelectedWorker.FirstName == Workers[i].FirstName)
                {
                    WorkerIndex = i;
                    break;
                }
            }
            var CloneOrder = SelectedOrder.Clone() as Order;

            int OrderIndex = Workers[WorkerIndex].GetOrderID(CloneOrder);

            if (OrderIndex != -1)
            {
                workerRepository.RemoveWorker(selectedWorker);
                selectedWorker.Orders[OrderIndex].IsDone = false;
                workerRepository.AddWorker(selectedWorker);

                WorkerOrdercollectionView.Refresh();
                SelectedOrder.IsDone = false;
            }

        }

        public bool Visibility { get; set; }
    }

 
}