using FileWizard.Gui.Infrastructure;
using FileWizard.Gui.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileWizard.Gui.WizardSteps
{
    public class FileListViewModel : ViewModelBase
    {
        private readonly INavigationManager _navigationManager;
        private readonly IFileRepository _fileRepository;
        private DelegateCommand _cancelCommand;
        private IEnumerable<FileData> backingFileData = Enumerable.Empty<FileData>();

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
            backingFileData = Enumerable.Empty<FileData>();
            FileList.Clear();
            PopulateFileList(e.FolderPath);
        }

        async Task PopulateFileList(string p)
        {
            IsBusy = true;

            backingFileData = await _fileRepository.GetFileDataAsync(p);

            AddItemsToList(backingFileData);
            if (!string.IsNullOrEmpty(SearchText))
            {
                FilterFileList(SearchText);
            }
            IsBusy = false;
        }

        private async Task<IEnumerable<FileData>> GetFileData(string path)
        {
            return _fileRepository.GetFileData(path);
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

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaiseOnPropertyChanged("IsBusy");
            }
        }
    }
}
