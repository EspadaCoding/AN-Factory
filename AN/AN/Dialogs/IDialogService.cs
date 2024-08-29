namespace AN.Dialogs
{
    public interface IDialogService
    {
        void Register<ViewModel, View>() where ViewModel : IDialogRequestClose
                                           where View : IDialog;

        bool? ShowDialog<ViewModel>(ViewModel viewModel) where ViewModel : IDialogRequestClose;
    }
}