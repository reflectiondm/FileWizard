using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileWizard.Gui.Infrastructure
{
    public class FileRepository : IFileRepository
    {
        public IEnumerable<FileData> GetFileData(string folderPath)
        {
            try
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
            catch (UnauthorizedAccessException)
            {
                return Enumerable.Empty<FileData>();
            }
        }

        public async Task<IEnumerable<FileData>> GetFileDataAsync(string folderPath)
        {
            return await Task.Factory.StartNew<IEnumerable<FileData>>(() => GetFileData(folderPath));
        }

        private FileData PopulateFileData(string file)
        {
            var fileInfo = new FileInfo(file);

            return new FileData()
            {
                Name = Path.GetFileNameWithoutExtension(file),
                Type = fileInfo.Extension,
                Size = fileInfo.Length,
                FullPath = Path.GetFullPath(file),
                Folder = fileInfo.DirectoryName,
                CreationTime = fileInfo.CreationTime,
                UpdateTime = fileInfo.LastWriteTime,
                AccessTime = fileInfo.LastAccessTime,
                IsReadonly = fileInfo.IsReadOnly
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
            return GetInnerFolders(folderPath).Any();
        }

        public IEnumerable<string> GetInnerFolders(string folderPath)
        {
            try
            {
                return Directory.GetDirectories(folderPath);
            }
            catch (UnauthorizedAccessException)
            {
                return Enumerable.Empty<string>();
            }
        }
    }
}
