using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui.Infrastructure
{
    public class FileRepository : IFileRepository
    {
        public IEnumerable<FileData> GetFileData(string folderPath)
        {
            throw new NotImplementedException();
        }

        public bool DoesFolderExist(string folderPath)
        {
            var path = Path.GetDirectoryName(folderPath);
            
            return folderPath == Path.GetPathRoot(folderPath) || 
                Directory.Exists(path);
        }

        public bool HaveInnerFolders(string folderPath)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetInnerFolders(string folderPath)
        {
            throw new NotImplementedException();
        }
    }
}
