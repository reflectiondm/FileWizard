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
            if (!DoesFolderExist(folderPath))
                throw new InvalidOperationException(string.Format("Folder {0} does not exist", folderPath));

            var files = Directory.GetFiles(folderPath);
            var result = new List<FileData>();
            foreach (var file in files)
            {
                var data = PopulateFileData(file);
                result.Add(data);
            }

            return result;
        }

        private FileData PopulateFileData(string file)
        {
            var fileInfo = new FileInfo(file);
            var fileName = Path.GetFileNameWithoutExtension(file);
            var type = fileInfo.Extension;
            var size = fileInfo.Length;
            return new FileData()
            {
                Name = fileName,
                Type = type,
                Size = size
            };
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
