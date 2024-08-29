using AN.Message;
using AN.Models;
using AN.PrintingView;
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
    public class AddModelViewModel : BaseViewModel , INotifyPropertyChanged
    {
        public IMessanger Messanger { get; set; }

        private ObservableCollection<WatchModel> models;
        public ObservableCollection<WatchModel> Models
        {
            get => models;
            set => ChangeProp(out models, value);
        }

        private WatchModel watchModel;
        public WatchModel WatchModel
        {
            get => watchModel;
            set
            {
                ChangeProp(out watchModel, value);
            }
        }

        public AddModelViewModel(IMessanger messanger)
        {
            this.Messanger = messanger;

            watchModel = new();
            
            date = DateTime.Now;

            Messanger.Subcribe<SendModelListMessage<WatchModel>>(i => {
                var message = i as SendModelListMessage<WatchModel>;
                models = message.List;
            });
        }

        private decimal totalPriceUSD;
        public decimal TotalPriceUSD
        {
            get => totalPriceUSD;
            set
            {
                ChangeProp(out totalPriceUSD, value);
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get => date;
            set
            {
                ChangeProp(out date, value);
            }
        }

        public decimal USDnum  { get; set; }


        private void RefreshDataGrid()
        {
            USDnum = 0;
            totalPriceUSD = 0;
            for (int i = 0; i < watchModel.Details.Count; i++)
            {
                if (i != 25)
                {
                    USDnum += watchModel.Details[i].USD;
                }
            }

            TotalPriceUSD = USDnum;
            
        }


        private void AddModel()
        {
            watchModel.TotalPrice = totalPriceUSD;
            watchModel.Name = watchModel.Name.Trim();
            ModelRepository modelRepository = new();

            modelRepository.AddModel(watchModel);
            models.Add(watchModel);
            Messanger.Send(new NavigationMessage { ViewModelType = typeof(ModelsViewModel) });
            Clear();
        }

        private void Clear()
        {
            watchModel = new();

        }

        private Command refreshDataGridCommand;
        public Command RefreshDataGridCommand => refreshDataGridCommand ??= new Command(() => { RefreshDataGrid(); });

        private Command addCommand;
        public Command AddCommand => addCommand ??= new Command(() => { AddModel(); });
        
        private Command navBack;
        public Command NavBack => navBack ??= new Command(() => { Messanger.Send(new NavigationMessage { ViewModelType = typeof(ModelsViewModel) }); });
    }
}
