using AN.Dialogs;
using AN.Models;
using AN.Repository;
using AN.Services;
using MVVMTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.ViewModels
{
    public class EditGoldCostWindowModel : BaseViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public IMessanger Messanger { get; set; }
        public IDialogService DialogService { get; set; }
        public IModelRepository ModelRepository { get; set; }

        public EditGoldCostWindowModel(IMessanger messanger, IDialogService dialogService, IModelRepository modelsRepository)
        {
            this.Messanger = messanger;
            this.DialogService = dialogService;
            this.ModelRepository = modelsRepository;
        }

        private decimal changing;
        public decimal Changing
        {
            get => changing;
            set
            {
                if (changing != value)
                {
                    changing = value;
                    ChangeProp(out changing, value);
                }
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

        public void SaveChanges()
        {
            ObservableCollection<WatchModel> ts =  new();

            for(int i = 0; i < ModelRepository.GetAllModels().Count;i++)
            {
                ts.Add(ModelRepository.GetAllModels()[i]);
            }
            ModelRepository.RemoveAllModels();

            for (int i = 0; i < ts.Count;i ++)
            {
                ts[i].TotalPrice -= ts[i].Details[15].USD + ts[i].Details[16].USD;
                ts[i].Details[15].USD = ts[i].Details[15].Gramms * changing;
                ts[i].Details[16].USD = ts[i].Details[16].Gramms * changing;
                ts[i].TotalPrice += ts[i].Details[15].USD + ts[i].Details[16].USD;
                ModelRepository.AddModel(ts[i]);
            }
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }
    }
}
