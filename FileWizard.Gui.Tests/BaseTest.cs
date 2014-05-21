using FileWizard.Gui.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui.Tests
{
    public abstract class BaseTest
    {
        protected NavigationManagerMock _navigationManagerMock;
        protected FileRepositoryMock _fileRepositoryMock;
        protected FakeUserInteractionManager _userInteractionManager;

        protected void BaseInit()
        {
            _navigationManagerMock = new NavigationManagerMock();
            _fileRepositoryMock = new FileRepositoryMock();
            _userInteractionManager = new FakeUserInteractionManager();
        }


        public void AssertSequencesAreEqual<T>(IEnumerable<T> left, IEnumerable<T> right,Func<T,T,bool> comparator = null)
        {
            if (comparator == null)
                comparator = (l,r) => l.Equals(r);

            var leftEn = left.GetEnumerator();
            var rightEn = right.GetEnumerator();
            int i = 0;
            while (leftEn.MoveNext())
            {
                if (!rightEn.MoveNext())
                    Assert.Fail("Sequences have different number of elements");

                if (!comparator(leftEn.Current, rightEn.Current))
                    Assert.Fail(string.Format("Sequences have different elements at index {0}", i));
                i++;
            }
        }
    }
}
