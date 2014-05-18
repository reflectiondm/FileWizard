using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileWizard.Gui.Tests.Fakes;

namespace FileWizard.Gui.Tests
{
    [TestClass]
    public class MainWindowViewModelTests : BaseTest
    {
        private FolderSelectorVMMock[] _viewModelMocks;
        private MainWindowViewModel _sut;
        private FolderSelectorVMMock _vm1;
        private FolderSelectorVMMock _vm2;

        [TestInitialize]
        public void TestInit()
        {
            BaseInit();
            _vm1 = new FolderSelectorVMMock();
            _vm2 = new FolderSelectorVMMock();
            _viewModelMocks = new[] {_vm1, _vm2 };
            _sut = new MainWindowViewModel(_viewModelMocks, _navigationManagerMock);
        }

        [TestMethod]
        public void OnStartup_ActiveViewModelIsFolderSelector()
        {
            var expectedActiveViewModel = _viewModelMocks[0];
            Assert.AreEqual(expectedActiveViewModel, _sut.ActiveViewModel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OnCreate_ThrowsArgumentException_WhenViewModelsIsEmpty()
        {
            _sut = new MainWindowViewModel(new IViewModel[0], _navigationManagerMock);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OnCreate_ThrowsArgumentException_WhenViewModelsIsNull()
        {
            _sut = new MainWindowViewModel(null, _navigationManagerMock);
        }

        [TestMethod]
        public void OnGoToNextEventRecieved_NextScreenBecomesActive()
        {
            _navigationManagerMock.GoToNextView();
            var result = _sut.ActiveViewModel;

            Assert.AreEqual(_viewModelMocks[1], result);
        }

        [TestMethod]
        public void OnGoToNextEventRecieved_NoNextScreen_ActiveScreenIsTheSame()
        {
            //arrange
            _navigationManagerMock.GoToNextView();
            //act
            _navigationManagerMock.GoToNextView();
            var result = _sut.ActiveViewModel;

            Assert.AreEqual(_viewModelMocks[1], result);
        }

        [TestMethod]
        public void OnGoToPreviousEventRecieved_PreviousScreenBecomesActive()
        {
            //arrange
            _navigationManagerMock.GoToNextView();
            //act
            _navigationManagerMock.GoToPreviousView();
            var result = _sut.ActiveViewModel;

            Assert.AreEqual(_viewModelMocks[0], result);
        }

        [TestMethod]
        public void OnGoToPreviousEventRecieved_NoPreviousScreen_ActiveScreenIsTheSame()
        {
            _navigationManagerMock.GoToPreviousView();

            var result = _sut.ActiveViewModel;

            Assert.AreEqual(_viewModelMocks[0], result);
        }
    }
}
