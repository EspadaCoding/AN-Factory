using AN.Message;
using AN.Services;
using MVVMTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public IMessanger Messanger { get; set; }
        public MenuViewModel(IMessanger messanger)
        {
            this.Messanger = messanger;
        }
        //переход на стр работников
        private Command navWorkers;
        public Command NavWorkers => navWorkers ??= new Command(() => { Messanger.Send<NavigationMessage>(new NavigationMessage { ViewModelType = typeof(WorkersViewModel) }); });

        private Command navOrders;
        public Command NavOrders => navOrders ??= new Command(() => {
            Messanger.Send<NavigationMessage>(new NavigationMessage { ViewModelType = typeof(OrdersViewModel) });
            Messanger.Send<RefreshOrdersMessage>(new RefreshOrdersMessage());
        });

        private Command navModels;
        public Command NavModels => navModels ??= new Command(() => {
            Messanger.Send<NavigationMessage>(new NavigationMessage { ViewModelType = typeof(ModelsViewModel) });
            
        });

    }
}
