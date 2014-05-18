using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileWizard.Gui.WizardSteps;

namespace FileWizard.Gui.Tests
{
    [TestClass]
    public class FileListViewModelTests : BaseTest
    {
        private FileListViewModel _sut;
        private string _folderPath;
        [TestInitialize]
        public void TestInit()
        {
            BaseInit();
            _folderPath = "SomePath";
            _sut = new FileListViewModel(_navigationManagerMock, _fileRepositoryMock);
        }

        [TestMethod]
        public void WhenFolderSelectedEventRecieved_GetFolderContentsFromRepository()
        {
            _navigationManagerMock.ChooseFolder(_folderPath);

            var expected = _fileRepositoryMock.GetFileData(_folderPath);

            var result = _sut.FileList;

            AssertSequencesAreEqual(expected, result, (l,r) => l.Name == r.Name);
        }
    }
}
