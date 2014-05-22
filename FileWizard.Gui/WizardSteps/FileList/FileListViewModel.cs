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
        private readonly IUserInteractionManager _userInteractionManager;
        private DelegateCommand _cancelCommand;
        private DelegateCommand _openFilesCommand;
        private DelegateCommand _showInFolderCommand;
        private DelegateCommand _copyToClipboadCommand;
        private DelegateCommand _showDetailsCommand;
        private List<FileData> _backingFileData = new List<FileData>();
        private IList<FileData> _selectedFiles;

        public FileListViewModel(INavigationManager navigationManager, IFileRepository fileRepository, IUserInteractionManager userInteractionManager)
        {
            _navigationManager = navigationManager;
            _navigationManager.OnFolderChosen += _navigationManager_OnFolderChosen;
            _fileRepository = fileRepository;
            _userInteractionManager = userInteractionManager;
            _cancelCommand = new DelegateCommand(d => _navigationManager.GoToPreviousView());
            _openFilesCommand = new DelegateCommand(d => OpenSelectedFiles(), d => CanOpenSelectedFiles());

            //Should any commands be added to context menu, it should be moved to separate menu view model.
            _showInFolderCommand = new DelegateCommand(d => ShowInFolder());
            _copyToClipboadCommand = new DelegateCommand(d => CopyToClipboard());
            _showDetailsCommand = new DelegateCommand(d => ShowDetails());
            FileList = new ObservableCollection<FileData>();
        }

        private void ShowDetails()
        {
            var fileData = SelectedItem;
            if (fileData == null)
                return;

            var detailsViewModel = new DetailsViewModel(SelectedItem);
            _userInteractionManager.ShowModalDialog(detailsViewModel, string.Format("Details: {0}", fileData.Name + fileData.Type));
        }

        private void CopyToClipboard()
        {
            var fileData = SelectedItem;
            if (fileData == null)
                return;

            fileData.CopyPathToClipboard();
        }

        private FileData _selectedItem;
        public FileData SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaiseOnPropertyChanged("SelectedItem");
            }
        }

        private void ShowInFolder()
        {
            var fileData = SelectedItem;
            if (fileData == null)
                return;

            fileData.OpenFolder();
        }

        private bool CanOpenSelectedFiles()
        {
            return SelectedFiles != null && SelectedFiles.Any();
        }

        private void OpenSelectedFiles()
        {
            if (SelectedFiles.Count > 10)
            {
                var shouldOpen = _userInteractionManager.AskUserConfirmation(string.Format("There seems to be a lot of files selected! Do you really want to open {0} files?", SelectedFiles.Count));
                if (!shouldOpen)
                    return;
            }

            foreach (var file in SelectedFiles)
            {
                file.Open();
            }
        }

        /// <summary>
        /// This is a property to set from view, because there is no simple and clean way to bind to selected items collection
        /// </summary>
        public IList<FileData> SelectedFiles
        {
            get { return _selectedFiles; }
            set
            {
                _selectedFiles = value;
                _openFilesCommand.RaiseCanExecuteChanged();
            }
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

            _backingFileData = new List<FileData>();
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

            _backingFileData.AddRange(fileData);

            AddItemsToList(fileData);
            if (!string.IsNullOrEmpty(SearchText))
            {
                FilterFileList(SearchText);
            }
            if (IsRecoursive && _fileRepository.HaveInnerFolders(folderPath))
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
            var data = _backingFileData.Where(d => Like(d.Name, _searchText) || Like(d.Type, _searchText));

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

        public ICommand OpenFilesCommand
        { get { return _openFilesCommand; } }

        public ICommand ShowInFolderCommand
        { get { return _showInFolderCommand; } }

        public ICommand CopyPathCommand
        { get { return _copyToClipboadCommand; } }

        public ICommand ShowDetailsCommand
        { get { return _showDetailsCommand; } }

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
