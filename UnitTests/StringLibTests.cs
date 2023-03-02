using StringManipulationLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class StringLibTests
    {
        [TestMethod]
        public void TestStringsSeparation1()
        {
            string Word = "One:Two:Three";


            List<string> ExpectedList = new List<string>();
            ExpectedList.Add("One");
            ExpectedList.Add("Two");
            ExpectedList.Add("Three");

            List<string> ActualList = StringLib.GetStringsSeparatedBy(':', Word);
            for (int i = 0; i < ExpectedList.Count; i++)
            {
                Assert.IsTrue(ActualList[i] == ExpectedList[i]);
            }
        }

        [TestMethod]
        public void TestStringsSeparation2()
        {
            string Word = "^One^Two^Three";


            List<string> ExpectedList = new List<string>();
            ExpectedList.Add("One");
            ExpectedList.Add("Two");
            ExpectedList.Add("Three");

            List<string> ActualList = StringLib.GetStringsSeparatedBy('^', Word);
            for (int i = 0; i < ExpectedList.Count; i++)
            {
                Assert.IsTrue(ActualList[i] == ExpectedList[i]);
            }
        }

        [TestMethod]
        public void TestStringsSeparation3()
        {
            string Word = "!One!Two!Three!";


            List<string> ExpectedList = new List<string>();
            ExpectedList.Add("One");
            ExpectedList.Add("Two");
            ExpectedList.Add("Three");

            List<string> ActualList = StringLib.GetStringsSeparatedBy('!', Word);
            for (int i = 0; i < ExpectedList.Count; i++)
            {
                Assert.IsTrue(ActualList[i] == ExpectedList[i]);
            }
        }

        [TestMethod]
        public void TestStringsSeparation4()
        {
            string Word = "!!!!!";


            Assert.IsTrue(StringLib.GetStringsSeparatedBy('!', Word).Count == 0);
        }

        [TestMethod]
        public void TestStringsSeparation5()
        {
            string Word = "";


            Assert.IsTrue(StringLib.GetStringsSeparatedBy('&', Word).Count == 0);
        }

        [TestMethod]
        public void TestStringsSeparation6()
        {
            string Word = "^One^Two^Three";


            Assert.IsTrue(StringLib.GetStringsSeparatedBy('&', Word)[0] == Word);
        }
    }
}
