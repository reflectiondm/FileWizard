using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileWizard.Gui.Infrastructure
{
    public class FileData
    {
        public DateTime CreationTime { get; set; }
        public FileData()
        {
            Name = "";
            Type = "";
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        public string Folder { get; set; }

        public string FullPath { get; set; }

        public virtual void Open()
        {
            Process.Start(FullPath);
        }

        public virtual void OpenFolder()
        {
            Process.Start(Folder);
        }

        public virtual void CopyPathToClipboard()
        {
            Clipboard.SetText(FullPath);
        }

        public DateTime UpdateTime { get; set; }
    }
}
