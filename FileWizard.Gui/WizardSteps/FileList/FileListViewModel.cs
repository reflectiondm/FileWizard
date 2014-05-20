using FileWizard.Gui.Infrastructure;
using FileWizard.Gui.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileWizard.Gui.WizardSteps
{
    public class FileListViewModel : ViewModelBase
    {
        private readonly INavigationManager _navigationManager;
        private readonly IFileRepository _fileRepository;
        private DelegateCommand _cancelCommand;
        private List<FileData> backingFileData = new List<FileData>();

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
            ClearData();
            _currentPath = e.FolderPath;
            PopulateFileList(_currentPath);
        }

        private void ClearData()
        {
            if (_cancellation != null && _cancellation.Token.CanBeCanceled)
                _cancellation.Cancel();

            backingFileData = new List<FileData>();
            FileList.Clear();
        }

        async Task PopulateFileList(string path)
        {
            try
            {
                IsBusy = true;
                _cancellation = new CancellationTokenSource();
                await PopulateFilesFromFolder(path, _cancellation.Token);
                IsBusy = false;
            }
            catch (OperationCanceledException ex)
            {
                IsBusy = false;   
                //throw;
            }
        }

        async Task PopulateFilesFromFolder(string folderPath, CancellationToken ct)
        {
            var fileData = await _fileRepository.GetFileDataAsync(folderPath);
            
            ct.ThrowIfCancellationRequested();

            backingFileData.AddRange(fileData);

            AddItemsToList(fileData);
            if (!string.IsNullOrEmpty(SearchText))
            {
                FilterFileList(SearchText);
            }
            if(IsRecoursive && _fileRepository.HaveInnerFolders(folderPath))
            {
                var innerFolders = _fileRepository.GetInnerFolders(folderPath);
                foreach (var folder in innerFolders)
                {
                    await PopulateFilesFromFolder(folder, ct);
                }
            }
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

        private bool _isRecoursive;
        private string _currentPath;
        private CancellationTokenSource _cancellation;
        public bool IsRecoursive
        {
            get { return _isRecoursive; }
            set
            {
                _isRecoursive = value;
                RaiseOnPropertyChanged("IsRecoursive");
                ClearData();
                PopulateFileList(_currentPath);
            }
        }
    }
}
