using FileWizard.Gui.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui.Tests.Fakes
{
    public class FakeFileData : FileData
    {
        public bool OpenWasCalled { get; set; }

        public override void Open()
        {
            OpenWasCalled = true;
        }

        public override void OpenFolder()
        {
            OpenFolderWasCalled = true;
        }

        public override void CopyPathToClipboard()
        {
            CopyPathWasCalled = true;
        }

        public bool OpenFolderWasCalled { get; set; }

        public bool CopyPathWasCalled { get; set; }
    }
}
