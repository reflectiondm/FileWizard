using FileWizard.Gui.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui.WizardSteps
{
    public class DetailsViewModel : ViewModelBase
    {
        private FileData _fileData;
        public DetailsViewModel(FileData fileData)
        {
            _fileData = fileData;
        }

        public FileData FileData
        {
            get { return _fileData; }
        }
    }
}
