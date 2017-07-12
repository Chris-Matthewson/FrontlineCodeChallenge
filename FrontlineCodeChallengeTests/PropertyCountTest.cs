using FrontlineCodeChallenge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrontlineCodeChallengeTests
{
    [TestClass]
    public class PropertyCountTest
    {
        /// <summary>
        ///     Tests for the correct depth
        /// </summary>
        [TestMethod]
        public void Test7Props()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("(id,employee(id,firstname,employeeType(id), lastname))");

            Assert.AreEqual(7, propertyGrp.PropertyCount);
        }
        
        /// <summary>
        ///     Tests for the correct depth
        /// </summary>
        [TestMethod]
        public void Test0Props()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("()");

            Assert.AreEqual(0, propertyGrp.PropertyCount);
        }
        /// <summary>
        ///     Tests for the correct depth
        /// </summary>
        [TestMethod]
        public void Test1Prop()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("(id");

            Assert.AreEqual(1, propertyGrp.PropertyCount);
        }
    }
}