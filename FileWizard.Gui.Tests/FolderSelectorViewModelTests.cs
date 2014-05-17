using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileWizard.Gui.FolderSelector;

namespace FileWizard.Gui.Tests
{
    [TestClass]
    public class FolderSelectorViewModelTests
    {
        FolderSelectorViewModel _sut;
        [TestInitialize]
        public void TestInit()
        {
            _sut = new FolderSelectorViewModel();
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
        public void WhenSelectedFolderIsNotEmpty_GoToNextStepIsEnabled()
        {
            _sut.FolderPath = "SomeFolderPath";

            var result = _sut.GoToNextStepCommand.CanExecute(null);

            Assert.IsTrue(result);
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
    }
}
