using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileWizard.Gui.Tests.Fakes;

namespace FileWizard.Gui.Tests
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        private FolderSelectorVMMock _folderSelectorViewModelMock;
        private NavigationManagerMock _navigationManagerMock;
        private MainWindowViewModel _sut;

        [TestInitialize]
        public void TestInit()
        {
            _folderSelectorViewModelMock = new FolderSelectorVMMock();
            _navigationManagerMock = new NavigationManagerMock();
            _sut = new MainWindowViewModel(_folderSelectorViewModelMock, _navigationManagerMock);
        }

        [TestMethod]
        public void OnStartup_ActiveViewModelIsFolderSelector()
        {
            var expectedActiveViewModel = _folderSelectorViewModelMock;
            Assert.AreEqual(expectedActiveViewModel, _sut.ActiveViewModel);
        }
    }
}
