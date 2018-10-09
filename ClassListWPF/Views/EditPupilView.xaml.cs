using System.Windows;
using ClassListWPF.Persistence;
using ClassListWPF.ViewModels;

namespace ClassListWPF.Views
{
    /// <summary>
    /// Interaction logic for EditPupilView.xaml
    /// </summary>
    public partial class EditPupilView : Window
    {
        private readonly EditPupilViewModel viewModel;

        public EditPupilView(EditPupilViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            DataContext = viewModel;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            viewModel.DeletePupil();
            Close();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            viewModel.SavePupil();
            Close();
        }
    }
}