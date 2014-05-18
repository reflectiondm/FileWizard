using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui.Infrastructure
{
    public class FileData
    {
        public FileData()
        {
            Name = "";
            Type = "";
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
    }
}
