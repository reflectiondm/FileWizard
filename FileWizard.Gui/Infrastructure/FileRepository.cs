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

        public async Task<IEnumerable<FileData>> GetFileDataAsync(string folderPath)
        {
            return await Task.Factory.StartNew<IEnumerable<FileData>>(() => GetFileData(folderPath));
        }

        private FileData PopulateFileData(string file)
        {
            var fileInfo = new FileInfo(file);
            var fileName = Path.GetFileNameWithoutExtension(file);
            var folder = fileInfo.DirectoryName;
            var type = fileInfo.Extension;
            var size = fileInfo.Length;
            var creationTime = fileInfo.CreationTime;
            var updateTime = fileInfo.LastWriteTime;

            //var sysIcon = System.Drawing.Icon.ExtractAssociatedIcon(file);
            //var wpfIcon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(sysIcon.Handle,
            //    System.Windows.Int32Rect.Empty,
            //    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            return new FileData()
            {
                Name = fileName,
                Type = type,
                Size = size,
                FullPath = file,
                Folder = folder,
                CreationTime= creationTime,
                UpdateTime = updateTime,
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
            return Directory.GetDirectories(folderPath);
        }
    }
}
