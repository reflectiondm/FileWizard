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
    }
}
