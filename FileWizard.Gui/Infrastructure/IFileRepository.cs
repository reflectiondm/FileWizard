using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui.Infrastructure
{
    public interface IFileRepository
    {
        IEnumerable<FileData> GetFileData(string path);

        bool DoesDirectoryExist(string path);

        bool HaveInnerDirectories(string path);

        IEnumerable<string> GetInnerDirectories(string path);
    }
}
