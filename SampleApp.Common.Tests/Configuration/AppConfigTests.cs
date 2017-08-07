using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Common.Configuration;

namespace SampleApp.Common.Tests.Configuration
{
    [TestClass()]
    public class AppConfigTests
    {
        private const string TestStringKey = "TestString";
        private const string TestStringValue = "test1";
        private const string TestIntKey = "TestInt";
        private const int TestIntValue= 10;

        [TestMethod()]
        public void GetConfigurationValueStringTest()
        {
            var config = new AppConfig();

            var resultString = config.GetConfigurationValue(TestStringKey);

            Assert.AreEqual(TestStringValue, resultString);

            var resultIntString = config.GetConfigurationValue(TestIntKey);

            Assert.AreEqual(TestIntValue.ToString(), resultIntString);
        }

        [TestMethod()]
        public void GetConfigurationValueTypeTest()
        {
            var config = new AppConfig();

            var resultInt= config.GetConfigurationValue<int>(TestIntKey);

            Assert.AreEqual(TestIntValue, resultInt);
        }

        [TestMethod()]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetConfigurationInvalidKeyTest()
        {
            var config = new AppConfig();
            var tmpKey = TestStringKey + "123";

            config.GetConfigurationValue(tmpKey);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void GetConfigurationInvalidTypeConversionTest()
        {
            var config = new AppConfig();
            var tmpKey = TestStringKey;

            config.GetConfigurationValue<int>(tmpKey);
        }

        [TestMethod()]
        public void GetConfigurationValueStringWithDefaultTest()
        {
            var config = new AppConfig();

            var resultString = config.GetConfigurationValue(TestStringKey, TestStringValue);

            Assert.AreEqual(TestStringValue, resultString);


            var tmpKey = TestIntKey + "123";
            var resultIntString = config.GetConfigurationValue(tmpKey, TestIntValue.ToString());

            Assert.AreEqual(TestIntValue.ToString(), resultIntString);
        }

        [TestMethod()]
        public void GetConfigurationValueTypeWithDefaultTest()
        {
            var config = new AppConfig();

            var resultString = config.GetConfigurationValue(TestStringKey, TestStringValue);

            Assert.AreEqual(TestStringValue, resultString);


            var tmpKey = TestIntKey + "123";
            var resultInt = config.GetConfigurationValue(tmpKey, TestIntValue);

            Assert.AreEqual(TestIntValue, resultInt);
        }
    }
}