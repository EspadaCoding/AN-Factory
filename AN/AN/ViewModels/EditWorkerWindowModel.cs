using AN.Dialogs;
using AN.Message;
using AN.Models;
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
    public class EditWorkerWindowModel : BaseViewModel, IDialogRequestClose
    {
        public IMessanger Messanger { get; set; }
        public IDialogService DialogService { get; set; }
        public IWorkerRepository WorkerRepository { get; set; }

        public EditWorkerWindowModel(IMessanger messanger, IDialogService dialogService)
        {
            this.Messanger = messanger;
            this.DialogService = dialogService;
        }

        public EditWorkerWindowModel(IMessanger messanger, Worker worker, IDialogService dialogService)
        {
            this.Messanger = messanger;
            this.DialogService = dialogService;
            this.SelectedWorker = worker;
            this.workerClone = worker.Clone() as Worker;
        }

        public EditWorkerWindowModel(IMessanger messanger, IDialogService dialogService, IWorkerRepository watchRepository)
        {
            this.Messanger = messanger;
            this.DialogService = dialogService;
            this.WorkerRepository = watchRepository;
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        private Worker selectedWorker;
        public Worker SelectedWorker
        {
            get => selectedWorker;
            set
            {
                ChangeProp(out selectedWorker, value);
            }
        }

        private Worker workerClone;
        public Worker WorkerClone
        {
            get => workerClone;
            set
            {
                ChangeProp(out workerClone, value);
            }
        }

        //Команды
        private Command cancerEditCommand;
        public Command CancerEditCommand => cancerEditCommand ??= new Command(() => { Cancer(); });

        private Command saveEditCommand;
        public Command SaveEditCommand => saveEditCommand ??= new Command(() => { SaveChanges(); });

        //Функции для комманд
        public void Cancer()
        {
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        public void SaveChanges()
        {

            WorkerRepository workerRepository = new();
            selectedWorker.FirstName =selectedWorker.FirstName.Trim();
            selectedWorker.LastName =selectedWorker.LastName.Trim();
            selectedWorker.Phone =selectedWorker.Phone.Trim();
            selectedWorker.SecondPhone =selectedWorker.SecondPhone.Trim();

            workerRepository.RemoveWorker(workerClone);
            workerRepository.AddWorker(selectedWorker);
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));

        }
    }
}
