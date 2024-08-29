using AN.Dialogs;
using AN.Repository;
using AN.Services;
using AN.ViewModels;
using AN.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace AN
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static IDialogService DialogService { get; set; }
        public static IWorkerRepository WorkerRepository { get; set; }
        public static IModelRepository ModelRepository { get; set; }

        private static IMessanger messanger;
        public static IMessanger Messanger { get => messanger; set => messanger = value; }

        private static MainViewModel mainViewModel;
        public static MainViewModel MainViewModel { get => mainViewModel; set => mainViewModel = value; }

        private static MenuViewModel menuViewModel;
        public static MenuViewModel MenuViewModel { get => menuViewModel; set => menuViewModel = value; }

        private static WorkersViewModel workersViewModel;
        public static WorkersViewModel WorkersViewModel { get => workersViewModel; set => workersViewModel = value; }

        private static AddModelViewModel addModelViewModel;
        public static AddModelViewModel AddModelViewModel { get => addModelViewModel; set => addModelViewModel = value; }

        private static ModelsViewModel modelsViewModel;
        public static ModelsViewModel ModelsViewModel { get => modelsViewModel; set => modelsViewModel = value; }

        private static EditModelViewModel editModelViewModel;
        public static EditModelViewModel EditModelViewModel { get => editModelViewModel; set => editModelViewModel = value; }

        private static LoginViewModel loginViewModel;
        public static LoginViewModel LoginViewModel { get => loginViewModel; set => loginViewModel = value; }

        private static OrdersViewModel ordersViewModel;
        public static OrdersViewModel OrdersViewModel { get => ordersViewModel; set => ordersViewModel = value; }

        private static AddOrderWindowModel addOrderWindowModel;
        public static AddOrderWindowModel AddOrderWindowModel { get => addOrderWindowModel; set => addOrderWindowModel = value; }

        private static EditWorkerWindowModel editWorkerWindowModel;
        public static EditWorkerWindowModel EditWorkerWindowModel { get => editWorkerWindowModel; set => editWorkerWindowModel = value; }

        private static OrderDetailsWindowModel orderDetailWindowModel;
        public static OrderDetailsWindowModel OrderDetailWindowModel { get =>orderDetailWindowModel; set => orderDetailWindowModel = value; }

        private static EditOrderWindowModel editOrderWindowModel;
        public static EditOrderWindowModel EditOrderWindowModel { get => editOrderWindowModel; set => editOrderWindowModel = value; }

        private static EditMetalWindowModel editMetalWindowModel;
        public static EditMetalWindowModel EditMetalWindowModel { get => editMetalWindowModel; set => editMetalWindowModel = value; }

        private static AcceptingWorkWindowModel acceptingWorkWindowModel;
        public static AcceptingWorkWindowModel AcceptingWorkWindowModel { get => acceptingWorkWindowModel; set => acceptingWorkWindowModel = value; }

        private static DoneOrderDetailsWindowModel doneOrderDetailsWindowModel;
        public static DoneOrderDetailsWindowModel DoneOrderDetailsWindowModel { get => doneOrderDetailsWindowModel; set => doneOrderDetailsWindowModel = value; }

        private static EditGoldCostWindowModel editGoldCostWindowModel;
        public static EditGoldCostWindowModel EditGoldCostWindowModel { get => editGoldCostWindowModel; set => editGoldCostWindowModel = value; }

        private static PartianNumberWindowModel partianNumberWindowModel;
        public static PartianNumberWindowModel PartianNumberWindowModel { get => partianNumberWindowModel; set => partianNumberWindowModel = value; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            

            DialogService = new DialogService(MainWindow);
            DialogService.Register<AddOrderWindowModel, AddOrderWindow>();
            DialogService.Register<EditWorkerWindowModel, EditWorkerWindow>();
            DialogService.Register<OrderDetailsWindowModel, OrderDetailsWindow>();
            DialogService.Register<EditOrderWindowModel, EditOrderWindow>();
            DialogService.Register<EditMetalWindowModel, EditMetalWindow>();
            DialogService.Register<AcceptingWorkWindowModel, AcceptingWorkWindow>();
            DialogService.Register<DoneOrderDetailsWindowModel, DoneOrderDetailsWindow>();
            DialogService.Register<EditGoldCostWindowModel, EditGoldCostWindow>();
            DialogService.Register<PartianNumberWindowModel, PartianNumberWindow>();

            WorkerRepository = new WorkerRepository();
            ModelRepository = new ModelRepository();
            Messanger = new Messanger();

            AddOrderWindowModel = new AddOrderWindowModel(Messanger, DialogService);
            EditWorkerWindowModel = new EditWorkerWindowModel(Messanger, DialogService, WorkerRepository);
            OrderDetailWindowModel = new OrderDetailsWindowModel(Messanger, DialogService);
            EditOrderWindowModel = new EditOrderWindowModel(Messanger, DialogService, WorkerRepository);
            EditMetalWindowModel = new EditMetalWindowModel(Messanger, DialogService, WorkerRepository);
            AcceptingWorkWindowModel = new AcceptingWorkWindowModel(Messanger, DialogService);
            DoneOrderDetailsWindowModel = new DoneOrderDetailsWindowModel(Messanger, DialogService);
            EditGoldCostWindowModel = new EditGoldCostWindowModel(Messanger, DialogService, ModelRepository);
            PartianNumberWindowModel = new PartianNumberWindowModel(Messanger, DialogService, WorkerRepository);
            

            
            
            MainViewModel = new MainViewModel(Messanger);
            MenuViewModel = new MenuViewModel(Messanger);
            WorkersViewModel = new WorkersViewModel(Messanger, WorkerRepository, DialogService);
            AddModelViewModel = new AddModelViewModel(Messanger);
            ModelsViewModel = new ModelsViewModel(Messanger,ModelRepository, DialogService);
            EditModelViewModel = new EditModelViewModel(Messanger,ModelRepository);
            LoginViewModel = new LoginViewModel(Messanger);
            OrdersViewModel = new OrdersViewModel(Messanger, WorkerRepository, DialogService);

            MainViewModel.CurrentViewModel = LoginViewModel;

            MainView mainView = new()
            {
                DataContext = MainViewModel
            };
            
            mainView.ShowDialog();

        }

        
    }
}
