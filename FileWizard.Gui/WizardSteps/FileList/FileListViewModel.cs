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
            FileList.Clear();
            var fileData = _fileRepository.GetFileData(p);

            foreach (var data in fileData)
            {
                FileList.Add(data);
            }
        }

        public ObservableCollection<FileData> FileList { get; private set; }

        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand;
            }
        }
    }
}
