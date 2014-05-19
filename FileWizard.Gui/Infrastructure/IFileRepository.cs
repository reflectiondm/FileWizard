using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui.Infrastructure
{
    public interface IFileRepository
    {
        IEnumerable<FileData> GetFileData(string folderPath);
        Task<IEnumerable<FileData>> GetFileDataAsync(string folderPath);

        bool DoesFolderExist(string folderPath);

        bool HaveInnerFolders(string folderPath);

        IEnumerable<string> GetInnerFolders(string folderPath);
    }
}
