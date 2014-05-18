using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui.Infrastructure
{
    public interface INavigationManager
    {
        void GoToNextView();

        void GoToPreviousView();

        void CloseWindow();

     

        event EventHandler OnToNextStep;

        event EventHandler OnToPreviousStep;


        //todo: next two members clearly indicate that navigation manager should be refactored into event aggregator.
        //it will do for now, though
        void ChooseFolder(string path);

        event EventHandler<FolderChosenEventArgs> OnFolderChosen;
    }

    public class FolderChosenEventArgs : EventArgs
    {
        public FolderChosenEventArgs(string folderPath)
        {
            FolderPath = folderPath;
        }
        public string FolderPath { get; private set; }
    }
}
