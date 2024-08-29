using AN.Message;
using AN.Services;
using MVVMTools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AN.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public IMessanger Messanger { get; set; }

        public LoginViewModel(IMessanger messanger)
        {
            Messanger = messanger;
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                ChangeProp(out password, value);
            }
        }

        private void CheckPassword()
        {
            string path = "Data/Data.json";
            string jsondata = File.ReadAllText(path);
            //string hash = JsonSerializer.Deserialize<>(jsondata);
            string ps = JObject.Parse(jsondata)["string"].ToString();
            

            if (password == ps)
            {
                Messanger.Send<NavigationMessage>(new NavigationMessage { ViewModelType = typeof(MenuViewModel) });
            }
        }

        private Command navMenu;
        public Command NavMenu => navMenu ??= new Command(() => { CheckPassword(); });
    }
}
