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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace AN.ViewModels
{
    public class WorkersViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IDialogService dialogService;
        public IWorkerRepository WorkerRepository { get; set; }
        public IMessanger Messanger { get; set; }
        public ICollectionView WorkerCollectionView { get; }

        public WorkersViewModel(IMessanger messanger, IWorkerRepository workerRepository, IDialogService dialogService)
        {
            this.Messanger = messanger;
            this.WorkerRepository = workerRepository;
            this.Workers = workerRepository.GetAllWorkers();
            this.dialogService = dialogService;
            EditWorkerDisplayMessageCommand = new ActionCommand(p => DisplayMessage());
            WorkerCollectionView = CollectionViewSource.GetDefaultView(workers);
        }
        private Worker worker;
        public Worker Worker
        {
            get => worker;
            set => ChangeProp(out worker, value);
        }
        private ObservableCollection<Worker> workers;
        public ObservableCollection<Worker> Workers
        {
            get => workers;
            set => ChangeProp(out workers, value);
        }

        private WorkersDetailsViewModel workersDetailsViewModel;
        public WorkersDetailsViewModel WorkersDetailsViewModel
        {
            get => workersDetailsViewModel;
            set => ChangeProp(out workersDetailsViewModel, value);
        }

        //поля для заполнения мы их связываем с текстБоксами во вьюшке и берём из них инфу
        private string editorName;
        public string EditorName
        {
            get=> editorName;
            set=> ChangeProp(out editorName, value);
        } 

        private string editorLastName;
        public string EditorLastName
        {
            get=> editorLastName;
            set=> ChangeProp(out editorLastName, value);
        }

        private string editorPhone1;
        public string EditorPhone1
        {
            get=> editorPhone1;
            set=> ChangeProp(out editorPhone1, value);
        }

        private string editorPhone2;
        public string EditorPhone2
        {
            get => editorPhone2;
            set => ChangeProp(out editorPhone2, value);
        }

        ///////////////////////////////////////////////////////////////////////////

        //Команда для появления окна изменения информации работника
        public ICommand EditWorkerDisplayMessageCommand { get; }
        private void DisplayMessage()
        {
            var viewModel = new EditWorkerWindowModel(Messanger, Worker, DialogService);

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
            WorkerRepository workerRepository = new();
            this.workers = workerRepository.GetAllWorkers();
            WorkerCollectionView.Refresh();
        }

        public IDialogService DialogService => dialogService;

        //Команда для добавления работника в список
        private Command addCommand;
        public Command AddCommand => addCommand ??= new Command(() => Add());

        // Команда для удаления работника из списка
         private Command delCommand;
         public Command DelCommand => delCommand ??= new Command(() => { Del(); });

        //команда для показания деталей работника
        private Command workerDetailCommand;
        public Command WorkerDetailCommand => workerDetailCommand ??=
            new Command(() => WorkersDetailsViewModel = new WorkersDetailsViewModel(Messanger,Worker,dialogService)
        );

        //переход на стр меню
        private Command navBack;
        public Command NavBack => navBack ??= new Command(() => { Messanger.Send<NavigationMessage>(new NavigationMessage { ViewModelType = typeof(MenuViewModel) }); });
        private void Clear()
        {
            this.EditorLastName = "";
            this.EditorName = "";
            this.EditorPhone1 = "";
            this.EditorPhone2 = "";
        }

        public void Add()
        {
            if (!string.IsNullOrWhiteSpace(EditorName) && !string.IsNullOrWhiteSpace(EditorLastName) && !string.IsNullOrWhiteSpace(EditorPhone1))
            {
                WorkerRepository.AddWorker(new Worker(EditorName, EditorLastName, EditorPhone1, EditorPhone2));
                Clear();
            }
        }



        public void Del()
        {
            WorkerRepository.RemoveWorker(Worker);
            Clear();
        }

    }
}
