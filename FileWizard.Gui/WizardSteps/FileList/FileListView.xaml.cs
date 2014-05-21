using FileWizard.Gui.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileWizard.Gui.WizardSteps
{
    /// <summary>
    /// Interaction logic for FileListView.xaml
    /// </summary>
    public partial class FileListView : UserControl
    {
        public FileListView()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = GetViewModel();
           
            var selectedFiles = new List<FileData>();
            foreach (FileData item in dataGrid.SelectedItems)
            {
                selectedFiles.Add(item);
            }

            viewModel.SelectedFiles = selectedFiles;
        }

        private void dataGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Right)
                return;

            var viewModel = GetViewModel();

            var row = sender as DataGridRow;

            viewModel.SelectedItem = row.Item as FileData;
        }

        FileListViewModel GetViewModel()
        {
            var viewModel = DataContext as FileListViewModel;
            if (viewModel == null)
                throw new InvalidOperationException("This view is supposed to work with FileListViewModel only");

            return viewModel;
        }
    }
}
