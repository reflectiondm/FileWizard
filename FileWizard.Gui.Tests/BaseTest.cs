using FileWizard.Gui.Tests.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui.Tests
{
    public abstract class BaseTest
    {
        protected NavigationManagerMock _navigationManagerMock;
        protected FileRepositoryMock _fileRepositoryMock;

        protected void BaseInit()
        {
            _navigationManagerMock = new NavigationManagerMock();
            _fileRepositoryMock = new FileRepositoryMock();
        }
    }
}
