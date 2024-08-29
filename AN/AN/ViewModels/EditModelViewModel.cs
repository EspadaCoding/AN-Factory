using AN.Message;
using AN.Models;
using AN.PrintingView;
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
    public class EditModelViewModel : BaseViewModel
    {
        public IMessanger Messanger { get; set; }
        public IModelRepository ModelRepository { get; set; }

        private WatchModel selectedModel;
        public WatchModel SelectedModel
        {
            get => selectedModel;
            set
            {
                if (selectedModel != value)
                {
                    selectedModel = value;
                    ChangeProp(out selectedModel, value);
                }

            }
        }

        public EditModelViewModel(IMessanger messanger,IModelRepository modelRepository)
        {
            Messanger = messanger;
            ModelRepository = modelRepository;

            Date = DateTime.Now;

            Messanger.Subcribe<ModelMessage>(i => {
                var message = i as ModelMessage;
                selectedModel = message.Model;
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

        public decimal USDnum { get; set; }

        private void RefreshDataGrid()
        {
            USDnum = 0;
            totalPriceUSD = 0;
            for (int i = 0; i < selectedModel.Details.Count; i++)
            {
                USDnum += selectedModel.Details[i].USD;


            }

            TotalPriceUSD = USDnum;

        }

        private void Print()
        {
            selectedModel.TotalPrice = totalPriceUSD;

            PrintModelView printCalcView = new(selectedModel, Date);
            System.Windows.Controls.PrintDialog printDialog = new();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(printCalcView, "Invoice");
            }
        }

        private void Save()
        {
            selectedModel.TotalPrice = totalPriceUSD;
            selectedModel.Name = selectedModel.Name.Trim();
            ModelRepository.RemoveModel(selectedModel);
            ModelRepository.AddModel(selectedModel);
            Messanger.Send(new NavigationMessage { ViewModelType = typeof(ModelsViewModel) });
        }


        private Command refreshDataGridCommand;
        public Command RefreshDataGridCommand => refreshDataGridCommand ??= new Command(() => { RefreshDataGrid(); });

        private Command printCommand;
        public Command PrintCommand => printCommand ??= new Command(() => { Print(); });

        private Command saveCommand;
        public Command SaveCommand => saveCommand ??= new Command(() => { Save(); });

        private Command navBack;
        public Command NavBack => navBack ??= new Command(() => { Messanger.Send(new NavigationMessage { ViewModelType = typeof(ModelsViewModel) }); });
    }
}
