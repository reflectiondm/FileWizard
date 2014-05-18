using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileWizard.Gui.WizardSteps;

namespace FileWizard.Gui.Tests
{
    [TestClass]
    public class FileListViewModelTests : BaseTest
    {
        private FileListViewModel _sut;

        [TestInitialize]
        public void TestInit()
        {
            BaseInit();
            _sut = new FileListViewModel();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
