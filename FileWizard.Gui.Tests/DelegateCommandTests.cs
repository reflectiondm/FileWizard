using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileWizard.Gui.Utils;

namespace FileWizard.Gui.Tests
{
    [TestClass]
    public class DelegateCommandTests
    {
        DelegateCommand _sut;

        bool executed;

        [TestInitialize]
        public void TestInitialize()
        {
            executed = false;
            _sut = new DelegateCommand(d => executed = true);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullEceptionIfExecuteActionIsNull()
        {
            _sut = new DelegateCommand(null);
        }

        [TestMethod]
        public void DelegateCommandInvokesExecuteActionOnExecuteCall()
        {
            _sut.Execute(null);

            Assert.IsTrue(executed);
        }

        [TestMethod]
        public void CanExecuteReturnsTrueIfNoDelegateWasGiven()
        {
            var result = _sut.CanExecute(null);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanExecuteReturnsResultOfTheFunctionIfItWasGiven()
        {
            _sut = new DelegateCommand(d => executed = true, d => false);
            
            var result = _sut.CanExecute(null);

            Assert.IsFalse(result);
        }

    }
}
