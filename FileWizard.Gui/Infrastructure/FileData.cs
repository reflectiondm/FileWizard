using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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

        public ImageSource Icon
        {
            get
            {
                var sysIcon = System.Drawing.Icon.ExtractAssociatedIcon(FullPath);
                var wpfIcon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(sysIcon.Handle,
                    System.Windows.Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                return wpfIcon;
            }
        }

        public DateTime AccessTime { get; set; }

        public bool IsReadonly { get; set; }
    }
}
