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
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => currentViewModel;
            set => ChangeProp(out currentViewModel, value);
        }

        public MainViewModel(IMessanger messanger)
        {
            messanger.Subcribe<NavigationMessage>(item =>
            {
                var message = item as NavigationMessage;
                var type = message.ViewModelType;

                if (type == typeof(MenuViewModel))
                {
                    CurrentViewModel = App.MenuViewModel;

                }
                if (type == typeof(WorkersViewModel))
                {
                    CurrentViewModel = App.WorkersViewModel;

                }
                if (type == typeof(AddModelViewModel))
                {
                    CurrentViewModel = App.AddModelViewModel;

                }
                if (type == typeof(ModelsViewModel))
                {
                    CurrentViewModel = App.ModelsViewModel;

                }
                if (type == typeof(EditModelViewModel))
                {
                    CurrentViewModel = App.EditModelViewModel;

                }
                if (type == typeof(LoginViewModel))
                {
                    CurrentViewModel = App.LoginViewModel;

                }
                if (type == typeof(OrdersViewModel))
                {
                    CurrentViewModel = App.OrdersViewModel;

                }
            });
        }
    }
}
