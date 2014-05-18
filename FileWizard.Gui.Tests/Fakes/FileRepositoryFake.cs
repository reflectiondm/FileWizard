using FileWizard.Gui.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui.Tests.Fakes
{
    public class FileRepositoryMock : IFileRepository
    {
        public bool SetupDoesFolderExist { get; set; }

        public IEnumerable<FileData> GetFileData(string path)
        {
            return new[]{
                new FileData(){Name = "File1"},
                new FileData(){Name = "File2"},
                new FileData(){Name = "File3"},
            };
        }

        public bool DoesFolderExist(string path)
        {
            return SetupDoesFolderExist;
        }

        public bool HaveInnerFolders(string path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetInnerFolders(string path)
        {
            throw new NotImplementedException();
        }
    }
}
