using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileWizard.Gui.WizardSteps;
using FileWizard.Gui.Tests.Fakes;

namespace FileWizard.Gui.Tests
{
    [TestClass]
    public class FolderSelectorViewModelTests : BaseTest
    {
        FolderSelectorViewModel _sut;

        [TestInitialize]
        public void TestInit()
        {
            BaseInit();
            _sut = new FolderSelectorViewModel(_navigationManagerMock, _fileRepositoryMock);
        }


        [TestMethod]
        public void WhenCreated_SelectedFolderIsEmpty()
        {
            var result = _sut.FolderPath;

            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void WhenCreated_GoNextCommandIsDisabled()
        {
            var result = _sut.GoToNextStepCommand.CanExecute(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenSelectedFolderIsNotEmpty_AndFolderExists_GoToNextStepIsEnabled()
        {
            _sut.FolderPath = "SomeFolderPath";
            _fileRepositoryMock.SetupDoesFolderExist = true;

            var result = _sut.GoToNextStepCommand.CanExecute(null);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WhenSelectedFolderIsNotEmpty_AndFolderDoesNotExist_GoToNextStepIsDisabled()
        {
            _sut.FolderPath = "SomeFolderPath";

            var result = _sut.GoToNextStepCommand.CanExecute(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenCreated_CancelCommandIsEnabled()
        {
            var result = _sut.CancelCommand.CanExecute(null);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WhenFolderPathChanges_GoToNextStepFiresCanExecuteChangedEvent()
        {
            var fired = false;
            _sut.GoToNextStepCommand.CanExecuteChanged += (s, e) => fired = true;

            _sut.FolderPath = "SomeFolderPath";

            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void WhenGoToNextInvoked_NavigateToFileList()
        {
            _sut.GoToNextStepCommand.Execute(null);

            Assert.IsTrue(_navigationManagerMock.GoNextWasCalled);
        }

        [TestMethod]
        public void WhenCancelCommandInvoked_CloseWindow()
        {
            _sut.CancelCommand.Execute(null);

            Assert.IsTrue(_navigationManagerMock.CloseWindowWasCalled);
        }
    }
}
