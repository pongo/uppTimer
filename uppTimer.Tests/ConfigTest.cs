using uppTimer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace uppTimer.Tests
{
    
    
    /// <summary>
    ///This is a test class for ConfigTest and is intended
    ///to contain all ConfigTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConfigTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ParseConfig
        ///</summary>
        [TestMethod()]
        public void ParseConfigTest()
        {
            // only minutes
            var target1 = new Config();
            target1.ParseConfig("name = 40m");
            Assert.AreEqual("name", target1.TimerName);
            Assert.AreEqual(TimeSpan.FromMinutes(40), target1.TotalTime);

            // hours and minutes
            var target2 = new Config();
            target2.ParseConfig("лала = 300h 40m");
            Assert.AreEqual("лала", target2.TimerName);
            Assert.AreEqual((TimeSpan.FromHours(300) + TimeSpan.FromMinutes(40)), target2.TotalTime);
        }

        /// <summary>
        ///A test for GetTimeString
        ///</summary>
        [TestMethod()]
        public void GetTimeStringTest()
        {
            Assert.AreEqual("0:00:00", Config.GetTimeString(TimeSpan.FromMinutes(0)));
            Assert.AreEqual("1:04:30", Config.GetTimeString(TimeSpan.FromHours(1) + TimeSpan.FromMinutes(4) + TimeSpan.FromSeconds(30)));
            Assert.AreEqual("100:44:00", Config.GetTimeString(TimeSpan.FromHours(100) + TimeSpan.FromMinutes(44)));

            //Assert.AreEqual("0m", Config.GetTimeString(TimeSpan.FromMinutes(0)));
            //Assert.AreEqual("40m", Config.GetTimeString(TimeSpan.FromMinutes(40)));
            //Assert.AreEqual("1h 40m", Config.GetTimeString((TimeSpan.FromHours(1) + TimeSpan.FromMinutes(40))));

            //// with seconds
            //Assert.AreEqual("30s", Config.GetTimeString(TimeSpan.FromSeconds(30), true));
            //Assert.AreEqual("44m 30s", Config.GetTimeString(TimeSpan.FromMinutes(44) + TimeSpan.FromSeconds(30), true));
            //Assert.AreEqual("1h", Config.GetTimeString(TimeSpan.FromHours(1), true));
            //Assert.AreEqual("1h 30s", Config.GetTimeString(TimeSpan.FromHours(1) + TimeSpan.FromSeconds(30), true));
            //Assert.AreEqual("1h 44m 30s", Config.GetTimeString(TimeSpan.FromHours(1) + TimeSpan.FromMinutes(44) + TimeSpan.FromSeconds(30), true));
            //Assert.AreEqual("1h 44m", Config.GetTimeString(TimeSpan.FromHours(1) + TimeSpan.FromMinutes(44) + TimeSpan.FromSeconds(0), true));
        }
    }
}
