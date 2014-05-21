using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileWizard.Gui.WizardSteps;
using System.Linq;
using FileWizard.Gui.Infrastructure;
using FileWizard.Gui.Tests.Fakes;
using System.Collections.Generic;

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
            _sut = new FileListViewModel(_navigationManagerMock, _fileRepositoryMock, _userInteractionManager);
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

            var expected = new[] { _fileRepositoryMock.GetFileData(_folderPath).First() };

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

        [TestMethod]
        public void WhenSelectedItemsEmpty_OpenFilesCannotExecute()
        {
            var result = _sut.OpenFilesCommand.CanExecute(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenSelectedItemsNotEmpty_OpenFilesCanExecute()
        {
            _sut.SelectedFiles = _fileRepositoryMock.GetFileData(_folderPath).ToList();

            var result = _sut.OpenFilesCommand.CanExecute(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WhenExecuteOpenFilesCommand_EachSelectedFileOpens()
        {
            _sut.SelectedFiles = _fileRepositoryMock.GetFileData(_folderPath).ToList();

            _sut.OpenFilesCommand.Execute(null);

            foreach (FakeFileData file in _sut.SelectedFiles)
            {
                Assert.IsTrue(file.OpenWasCalled);
            }
        }

        [TestMethod]
        public void WhenExecuteOpenFilesCommand_MoreThenTenSelectedFiles_AskUsersConfirmation()
        {
            //ok, that was not on the requirements list, but 
            //I just can't let the user open thousands of files without giving a chance to reconsider such an action
            var selected = new List<FileData>();
            for (int i = 0; i < 15; i++)
            {
                selected.Add(new FakeFileData());
            }
            _sut.SelectedFiles = selected;

            _sut.OpenFilesCommand.Execute(null);

            Assert.IsTrue(_userInteractionManager.UserConfirmationAsked);
        }

        [TestMethod]
        public void WhenFileSelected_AndOpenFolderCommandInvoked_FileDataOpensFolder()
        {
            var fileDataMock = new FakeFileData();
            _sut.SelectedItem = fileDataMock;

            _sut.ShowInFolderCommand.Execute(null);

            Assert.IsTrue(fileDataMock.OpenFolderWasCalled);
        }

        [TestMethod]
        public void WhenFileSelected_AndCopyPathCommandInvoked_FileDataOpensFolder()
        {
            var fileDataMock = new FakeFileData();
            _sut.SelectedItem = fileDataMock;

            _sut.CopyPathCommand.Execute(null);

            Assert.IsTrue(fileDataMock.CopyPathWasCalled);
        }

        [TestMethod]
        public void ShowDetailsInvoked_UserInteractionManager_ShowModalDialogInvoked()
        {
            var fileDataMock = new FakeFileData();
            _sut.SelectedItem = fileDataMock;
            _userInteractionManager.ShowModal_ExpectedTitle = "File details";
            _sut.ShowDetailsCommand.Execute(null);

            Assert.IsTrue(_userInteractionManager.ShowModalDialogWasCalled);
        }
    }
}
