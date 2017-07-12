using FrontlineCodeChallenge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrontlineCodeChallengeTests
{
    [TestClass]
    public class DepthTests
    {
        /// <summary>
        ///     Tests for the correct depth
        /// </summary>
        [TestMethod]
        public void Test3Depth()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("(id,created,employee(id,firstname,employeeType(id), lastname),location)");

            Assert.AreEqual(2, propertyGrp.MaxDepth);
        }
        
        /// <summary>
        ///     Tests for the correct depth
        /// </summary>
        [TestMethod]
        public void Test2Depth()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("(id,created,employee(id,firstname,employeeType, lastname),location)");

            Assert.AreEqual(1, propertyGrp.MaxDepth);
        }
        /// <summary>
        ///     Tests for the correct depth
        /// </summary>
        [TestMethod]
        public void Test1Depth()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("(id,created,employee,id,firstname,employeeType, lastname,location)");

            Assert.AreEqual(0, propertyGrp.MaxDepth);
        }
    }
}
