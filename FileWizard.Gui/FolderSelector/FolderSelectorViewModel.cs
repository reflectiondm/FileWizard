using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui.FolderSelector
{
    public class FolderSelectorViewModel : ViewModelBase
    {
        public FolderSelectorViewModel()
        {
            FolderPath = "This is FolderPath. Binding works";
        }

        private string _folderPath;
        public string FolderPath
        {
            get
            {
                return _folderPath;
            }
            set
            {
                _folderPath = value;
                RaiseOnPropertyChanged("FolderPath");
            }
        }
    }
}
