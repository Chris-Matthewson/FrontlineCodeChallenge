using FrontlineCodeChallenge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FrontlineCodeChallengeTests
{
    [TestClass]
    public class NonAlphabeticalTests
    {
        /// <summary>
        ///     Tests the provided string
        /// </summary>
        [TestMethod]
        public void TestProvidedString()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("(id,created,employee(id,firstname,employeeType(id), lastname),location)");

            var expected = "id" + Environment.NewLine +
                           "created" + Environment.NewLine +
                           "employee" + Environment.NewLine +
                           "-id" + Environment.NewLine +
                           "-firstname" + Environment.NewLine +
                           "-employeeType" + Environment.NewLine +
                           "--id" + Environment.NewLine +
                           "-lastname" + Environment.NewLine +
                           "location" + Environment.NewLine;

            Assert.AreEqual(expected, propertyGrp.ToString());
        }

        /// <summary>
        ///     Tests for the user adding too many open parenthesis
        /// </summary>
        [TestMethod]
        public void TestTooManOpenParens()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("(((id,created,employee(id,firstname,employeeType(id), lastname),location)");

            var expected = "id" + Environment.NewLine +
                           "created" + Environment.NewLine +
                           "employee" + Environment.NewLine +
                           "-id" + Environment.NewLine +
                           "-firstname" + Environment.NewLine +
                           "-employeeType" + Environment.NewLine +
                           "--id" + Environment.NewLine +
                           "-lastname" + Environment.NewLine +
                           "location" + Environment.NewLine;

            Assert.AreEqual(expected, propertyGrp.ToString());
        }

        /// <summary>
        ///     Tests for the user adding too many close parenthesis
        /// </summary>
        [TestMethod]
        public void OpenAndCloseParens()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("()(id,created,employee(id,firstname,employeeType(id), lastname),location)");

            var expected = "id" + Environment.NewLine +
                           "created" + Environment.NewLine +
                           "employee" + Environment.NewLine +
                           "-id" + Environment.NewLine +
                           "-firstname" + Environment.NewLine +
                           "-employeeType" + Environment.NewLine +
                           "--id" + Environment.NewLine +
                           "-lastname" + Environment.NewLine +
                           "location" + Environment.NewLine;

            Assert.AreEqual(expected, propertyGrp.ToString());
        }

        /// <summary>
        ///     Tests to make sure ignored characters are actually ignored
        /// </summary>
        [TestMethod]
        public void TestIgnoredCharacters()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("(id,created,\"employee(id,  firstn  ame,employe   eType(id ), lastname),location)");

            var expected = "id" + Environment.NewLine +
                           "created" + Environment.NewLine +
                           "employee" + Environment.NewLine +
                           "-id" + Environment.NewLine +
                           "-firstname" + Environment.NewLine +
                           "-employeeType" + Environment.NewLine +
                           "--id" + Environment.NewLine +
                           "-lastname" + Environment.NewLine +
                           "location" + Environment.NewLine;

            Assert.AreEqual(expected, propertyGrp.ToString());
        }

        /// <summary>
        ///     Tests to make sure it is ok if the user does not close all parenthesis
        /// </summary>
        [TestMethod]
        public void TestUnclosedString()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("(test,id,employee(id,employeeType(id), lastname");

            var expected = "test" + Environment.NewLine +
                           "id" + Environment.NewLine +
                           "employee" + Environment.NewLine +
                           "-id" + Environment.NewLine +
                           "-employeeType" + Environment.NewLine +
                           "--id" + Environment.NewLine +
                           "-lastname" + Environment.NewLine;

            Assert.AreEqual(expected, propertyGrp.ToString());
        }

        /// <summary>
        ///     Tests to make sure it is ok if the users forgets to open parenthesis
        /// </summary>
        [TestMethod]
        public void TestUnopenedString()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("test,id,employee(id,employeeType(id), lastname))");

            var expected = "test" + Environment.NewLine +
                           "id" + Environment.NewLine +
                           "employee" + Environment.NewLine +
                           "-id" + Environment.NewLine +
                           "-employeeType" + Environment.NewLine +
                           "--id" + Environment.NewLine +
                           "-lastname" + Environment.NewLine;

            Assert.AreEqual(expected, propertyGrp.ToString());
        }
    }
}