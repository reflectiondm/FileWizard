using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileWizard.Gui.WizardSteps;
using System.Linq;
using FileWizard.Gui.Infrastructure;

namespace FileWizard.Gui.Tests
{
    [TestClass]
    public class FileListViewModelTests : BaseTest
    {
        private FileListViewModel _sut;
        private string _folderPath;
        Func<FileData, FileData, bool> _comparator = (l, r) => l.Name == r.Name;

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

            AssertSequencesAreEqual(expected, result, _comparator);
        }

        [TestMethod]
        public void WhenSearchTextIsChanged_FileListGetsFiltered()
        {
            _navigationManagerMock.ChooseFolder(_folderPath);
            _sut.SearchText = "1";

            var expected = new[]{_fileRepositoryMock.GetFileData(_folderPath).First()};

            AssertSequencesAreEqual(expected, _sut.FileList, _comparator);
        }

        [TestMethod]
        public void WhenSearchTextIsChanged_FileListGetsFiltered_CaseInsensitive()
        {
            _navigationManagerMock.ChooseFolder(_folderPath);
            _sut.SearchText = "FILE1";

            var expected = new[] { _fileRepositoryMock.GetFileData(_folderPath).First() };

            AssertSequencesAreEqual(expected, _sut.FileList, _comparator);
        }

        [TestMethod]
        public void WhenSearchTextIsChanged_FileListGetsFiltered_CanSearchByExtension()
        {
            _navigationManagerMock.ChooseFolder(_folderPath);
            _sut.SearchText = "exe";

            var expected = new[] { _fileRepositoryMock.GetFileData(_folderPath).First() };

            AssertSequencesAreEqual(expected, _sut.FileList, _comparator);
        }

        [TestMethod]
        public void WhenIsRecoursiveSearchIsChangedToTrue_ConentsOfInnerFoldersAreAdded()
        {
            _navigationManagerMock.ChooseFolder(_folderPath);
            _sut.IsRecoursive = true;

            var expected = _fileRepositoryMock.GetFileData(_folderPath).Concat(_fileRepositoryMock.GetFileData("inner"));

            AssertSequencesAreEqual(expected, _sut.FileList, _comparator);
        }
    }
}
