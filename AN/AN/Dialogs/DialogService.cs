using System;
using System.Collections.Generic;
using System.Windows;

namespace AN.Dialogs
{

    public class DialogService : IDialogService
    {
        private readonly Window owner;

        public DialogService(Window owner)
        {
            this.owner = owner;
            Mappings = new Dictionary<Type, Type>();
        }

        public IDictionary<Type, Type> Mappings { get; }

        public void Register<ViewModel, View>() where ViewModel : IDialogRequestClose
                                                  where View : IDialog
        {
            if (Mappings.ContainsKey(typeof(ViewModel)))
            {
                throw new ArgumentException($"ype {typeof(ViewModel)} is already mapped to type {typeof(View)}");
            }

            Mappings.Add(typeof(ViewModel), typeof(View));
        }

        public bool? ShowDialog<ViewModel>(ViewModel viewModel) where ViewModel : IDialogRequestClose
        {
            Type viewype = Mappings[typeof(ViewModel)];

            IDialog dialog = (IDialog)Activator.CreateInstance(viewype);

            EventHandler<DialogCloseRequestedEventArgs> handler = null;

            handler = (sender, e) =>
            {
                viewModel.CloseRequested -= handler;

                if (e.DialogResult.HasValue)
                {
                    dialog.DialogResult = e.DialogResult;
                }
                else
                {
                    dialog.Close();
                }
            };

            viewModel.CloseRequested += handler;

            dialog.DataContext = viewModel;
            dialog.Owner = owner;

            return dialog.ShowDialog();
        }
    }
}