using AN.Dialogs;
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
using System.Windows.Controls;
using System.Windows.Data;

namespace AN.ViewModels
{
    public class ModelsViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public IMessanger Messanger { get; set; }
        public IModelRepository ModelRepository { get; set; }
        public ICollectionView ModelCollectionView { get; }
        private readonly IDialogService DialogService;

        private ObservableCollection<WatchModel> models;
        public ObservableCollection<WatchModel> Models
        {
            get => models;
            set => ChangeProp(out models, value);
        }

        private WatchModel selectedModel;
        public WatchModel SelectedModel
        {
            get => selectedModel;
            set => ChangeProp(out selectedModel, value);
        }

        public ModelsViewModel(IMessanger messanger , IModelRepository modelRepository , IDialogService dialogService)
        {
            this.Messanger = messanger;
            ModelRepository = modelRepository;
            DialogService = dialogService;

            models = modelRepository.GetAllModels();

            ModelCollectionView = CollectionViewSource.GetDefaultView(models);

            ModelCollectionView.Filter = FilterModels;
        }


        private void DeleteModel()
        {
            ModelRepository.RemoveModel(selectedModel);

        }

        private void PrintModel()
        {

            PrintModelView printCalcView = new(selectedModel.Clone() as WatchModel);
            System.Windows.Controls.PrintDialog printDialog = new();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(printCalcView, "Invoice");
            }
        }

        private void EditGold()
        {
            var viewModel = new EditGoldCostWindowModel(Messanger, DialogService, ModelRepository);

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
            models = ModelRepository.GetAllModels();
        }

        private void PrintAllModel()
        {
            int reminder = models.Count % 26;
            int divider = models.Count / 26;

            List<List<WatchModel>> splitedmodels = new List<List<WatchModel>>();

            int b = 0; 
            for(int i = 0; i < divider; i ++)
            {
                splitedmodels.Add(new List<WatchModel>());
                for(int n = 0; n < 26; n++, b++)
                {
                    splitedmodels[i].Add(models[b]);
                }
            }

            if(reminder != 0)
            {
                splitedmodels.Add(new List<WatchModel>());
                for(int i = 0, n = models.Count - reminder; i < reminder;i++ , n++)
                {
                    splitedmodels[^1].Add(models[n]);
                }
            }
            PrintDialog printDialog = new();
            if(printDialog.ShowDialog() == true)
            {
                for(int i = 0; i < divider; i++ )
                {
                    PrintAllModelsView printAllModelsView = new PrintAllModelsView(splitedmodels[i], i +1);
                    printDialog.PrintVisual(printAllModelsView, "Invoke");
                }
                if(reminder != 0)
                {
                    PrintAllModelsView printAllModelsView = new PrintAllModelsView(splitedmodels[^1], splitedmodels.Count);
                    printDialog.PrintVisual(printAllModelsView, "Invoke");
                }
            }
        }

        private Command navBack;
        public Command NavBack => navBack ??= new Command(() => { Messanger.Send<NavigationMessage>(new NavigationMessage { ViewModelType = typeof(MenuViewModel) }); });

        private Command delCommand;
        public Command DelCommand => delCommand ??= new Command(() => { DeleteModel(); });

        private Command printCommand;
        public Command PrintCommand => printCommand ??= new Command(() => { PrintModel(); });

        private Command printAllModelsCommand;
        public Command PrintAllModelsCommand => printAllModelsCommand ??= new Command(() => { PrintAllModel(); });

        private Command navAddModel;
        public Command NavAddModel => navAddModel ??= new Command(() =>
        {
            Messanger.Send<NavigationMessage>(new NavigationMessage
            {
                ViewModelType = typeof(AddModelViewModel)
            });
            Messanger.Send<SendModelListMessage<WatchModel>>(new SendModelListMessage<WatchModel>
            {
                List = models
            });
        });

        private Command editModelCommand;
        public Command EditModelCommand => editModelCommand ??= new Command(() =>
        {
            Messanger.Send<NavigationMessage>(new NavigationMessage
            {
                ViewModelType = typeof(EditModelViewModel)
            });
            Messanger.Send<ModelMessage>(new ModelMessage
            {
                Model = selectedModel
            });
        });

        private Command editGoldCommand;
        public Command EditGoldCommand => editGoldCommand ??= new Command(() => { EditGold(); });

        private string _ModelFilter = string.Empty;
        public string ModelFilter
        {
            get => _ModelFilter;
            set
            {

                ChangeProp(out _ModelFilter, value);
                ModelCollectionView.Refresh();
            }
        }


        private bool FilterModels(object obj)
        {
            if (obj is WatchModel model)
            {
                if (_ModelFilter == "")
                {
                    return true;
                }
                else
                {
                    return model.Name == _ModelFilter;
                }
            }
            return false;
        }

    }
}
