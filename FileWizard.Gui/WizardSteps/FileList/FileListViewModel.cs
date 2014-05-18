using FileWizard.Gui.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FileWizard.Gui.Utils;

namespace FileWizard.Gui.WizardSteps
{
    public class FileListViewModel : ViewModelBase
    {
        private readonly INavigationManager _navigationManager;
        private readonly IFileRepository _fileRepository;
        private DelegateCommand _cancelCommand;
        private IEnumerable<FileData> backingFileData = new FileData[0];

        public FileListViewModel(INavigationManager navigationManager, IFileRepository fileRepository)
        {
            _navigationManager = navigationManager;
            _navigationManager.OnFolderChosen += _navigationManager_OnFolderChosen;
            _fileRepository = fileRepository;
            _cancelCommand = new DelegateCommand(d => _navigationManager.GoToPreviousView());
            FileList = new ObservableCollection<FileData>();
        }

        void _navigationManager_OnFolderChosen(object sender, FolderChosenEventArgs e)
        {
            PopulateFileList(e.FolderPath);
        }

        private void PopulateFileList(string p)
        {
            backingFileData = _fileRepository.GetFileData(p);

            AddItemsToList(backingFileData, true);
        }

        private void AddItemsToList(IEnumerable<FileData> fileData, bool shouldClear = false)
        {
            if (shouldClear)
                FileList.Clear();

            foreach (var data in fileData)
            {
                FileList.Add(data);
            }
        }

        public ObservableCollection<FileData> FileList { get; private set; }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                FilterFileList(_searchText);
                RaiseOnPropertyChanged("SearchText");
            }
        }

        private void FilterFileList(string _searchText)
        {
            var data = backingFileData.Where(d => Like(d.Name, _searchText) || Like(d.Type, _searchText));

            AddItemsToList(data, true);
        }

        private bool Like(string left, string right)
        {
            return left.ToLower().Contains(right.ToLower());
        }

        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand;
            }
        }
    }
}
