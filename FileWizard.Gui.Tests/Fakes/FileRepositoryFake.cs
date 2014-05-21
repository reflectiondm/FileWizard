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
            if (path == "inner")
                return new[] { new FakeFileData() { Name = "File4", Type = "zip" }, };

            return new[]{
                new FakeFileData(){Name = "File1", Type = "EXE"},
                new FakeFileData(){Name = "File2", Type = "zip"},
                new FakeFileData(){Name = "File3", Type = "txt"},
            };
        }

        public bool DoesFolderExist(string path)
        {
            return SetupDoesFolderExist;
        }

        public bool HaveInnerFolders(string path)
        {
            if (path == "inner")
                return false;

            return true;
        }

        public IEnumerable<string> GetInnerFolders(string path)
        {
            if (path == "inner")
                return Enumerable.Empty<string>();

            return new[] { "inner" };
        }


        public Task<IEnumerable<FileData>> GetFileDataAsync(string folderPath)
        {
            return Task.Factory.StartNew<IEnumerable<FileData>>(
                () => GetFileData(folderPath));
        }
    }
}
