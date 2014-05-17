using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileWizard.Gui.Tests
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        private FolderSelectorVMMock folderSelectorViewModelMock;
        private MainWindowViewModel sut;

        [TestInitialize]
        public void TestInit()
        {
            folderSelectorViewModelMock = new FolderSelectorVMMock();
            sut = new MainWindowViewModel(folderSelectorViewModelMock);
        }

        [TestMethod]
        public void OnStartup_ActiveViewModelIsFolderSelector()
        {
            var expectedActiveViewModel = folderSelectorViewModelMock;
            Assert.AreEqual(expectedActiveViewModel, sut.ActiveViewModel);
        }
    }
}
