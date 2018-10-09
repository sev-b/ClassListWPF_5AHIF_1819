using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using ClassListWPF.Models;
using ClassListWPF.Persistence;
using ClassListWPF.ViewModels;
using ClassListWPF.Views;
using Microsoft.Win32;

namespace ClassListWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Field

        private readonly MainViewModel viewModel;

        #endregion

        #region Public Constructor

        public MainWindow()
        {
            new ClassListDbContext().Database.EnsureCreated();
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        #endregion

        #region Public Method

        public void OpenFile(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "CSV Files(*.csv)|*.csv|All Files (*.*)|*.*",
                RestoreDirectory = true
            };

            if (dlg.ShowDialog() == true)
            {
                Import(dlg.FileName);
            }

            ;
        }

        private async void Import(string dlgFileName)
        {
            ProgressBar.Visibility = Visibility.Visible;
            DataGetter.Progress = percent =>
            {
                ProgressBar.Dispatcher.Invoke(() => ProgressBar.Value = percent);
            };
            await DataGetter.ImportAsync(dlgFileName);
            viewModel.SelectedForm = new Form();
            viewModel.UpdateForms();
            ProgressBar.Visibility = Visibility.Collapsed;
        }

        #endregion

        private void EditPupil(object sender, MouseButtonEventArgs e)
        {
            var editPupilWindow = new EditPupilView(new EditPupilViewModel(viewModel.SelectedPupil));
            editPupilWindow.Show();
            editPupilWindow.Closing += (s, ed) =>
            {
                viewModel.UpdatePupils();
                viewModel.UpdateForms();
            };
        }

        #region Private Method

        private void SelectClass(object sender, RoutedEventArgs e)
        {
            if (Forms.SelectedItem == null)
            {
                viewModel.UpdatePupils();
                return;
            }

            viewModel.SelectedForm = FormRepository.GetInstance().GetForms().Single(f => f.Id == ((Form)Forms.SelectedItem).Id);
            viewModel.UpdatePupils();
        }

        #endregion
    }
}