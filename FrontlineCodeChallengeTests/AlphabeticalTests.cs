using FrontlineCodeChallenge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FrontlineCodeChallengeTests
{
    [TestClass]
    public class AlphabeticalTests
    {
        /// <summary>
        ///     Tests the provided string
        /// </summary>
        [TestMethod]
        public void TestProvidedStringAlphabetical()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("(id,created,employee(id,firstname,employeeType(id), lastname),location)");

            var expected = "created" + Environment.NewLine +
                           "employee" + Environment.NewLine +
                           "-employeeType" + Environment.NewLine +
                           "--id" + Environment.NewLine +
                           "-firstname" + Environment.NewLine +
                           "-id" + Environment.NewLine +
                           "-lastname" + Environment.NewLine + 
                           "id" + Environment.NewLine +
                           "location" + Environment.NewLine;

            Assert.AreEqual(expected, propertyGrp.ConvertToString(true));
        }

        /// <summary>
        ///     Tests for the user adding too many open parenthesis
        /// </summary>
        [TestMethod]
        public void TestTooManOpenParensAlphabetical()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("(((id,created,employee(id,firstname,employeeType(id), lastname),location)");

            var expected = "created" + Environment.NewLine +
                           "employee" + Environment.NewLine +
                           "-employeeType" + Environment.NewLine +
                           "--id" + Environment.NewLine +
                           "-firstname" + Environment.NewLine +
                           "-id" + Environment.NewLine +
                           "-lastname" + Environment.NewLine +
                           "id" + Environment.NewLine +
                           "location" + Environment.NewLine;

            Assert.AreEqual(expected, propertyGrp.ConvertToString(true));
        }

        /// <summary>
        ///     Tests for the user adding too many close parenthesis
        /// </summary>
        [TestMethod]
        public void OpenAndCloseParensAlphabetical()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("()(id,created,employee(id,firstname,employeeType(id), lastname),location)");

            var expected = "created" + Environment.NewLine +
                           "employee" + Environment.NewLine +
                           "-employeeType" + Environment.NewLine +
                           "--id" + Environment.NewLine +
                           "-firstname" + Environment.NewLine +
                           "-id" + Environment.NewLine +
                           "-lastname" + Environment.NewLine +
                           "id" + Environment.NewLine +
                           "location" + Environment.NewLine;

            Assert.AreEqual(expected, propertyGrp.ConvertToString(true));
        }

        /// <summary>
        ///     Tests to make sure ignored characters are actually ignored
        /// </summary>
        [TestMethod]
        public void TestIgnoredCharactersAlphabetical()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("(id,created,\"employee(id,  firstn  ame,employe   eType(id ), lastname),location)");

            var expected = "created" + Environment.NewLine +
                           "employee" + Environment.NewLine +
                           "-employeeType" + Environment.NewLine +
                           "--id" + Environment.NewLine +
                           "-firstname" + Environment.NewLine +
                           "-id" + Environment.NewLine +
                           "-lastname" + Environment.NewLine +
                           "id" + Environment.NewLine +
                           "location" + Environment.NewLine;

            Assert.AreEqual(expected, propertyGrp.ConvertToString(true));
        }

        /// <summary>
        ///     Tests to make sure it is ok if the user does not close all parenthesis
        /// </summary>
        [TestMethod]
        public void TestUnclosedStringAlphabetical()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("(test,id,employee(id,employeeType(id), lastname");

            var expected = "employee" + Environment.NewLine +
                           "-employeeType" + Environment.NewLine +
                           "--id" + Environment.NewLine +
                           "-id" + Environment.NewLine +
                           "-lastname" + Environment.NewLine +
                           "id" + Environment.NewLine +
                           "test" + Environment.NewLine;


            Assert.AreEqual(expected, propertyGrp.ConvertToString(true));
        }

        /// <summary>
        ///     Tests to make sure it is ok if the users forgets to open parenthesis
        /// </summary>
        [TestMethod]
        public void TestUnopenedStringAlphabetical()
        {
            var propertyGrp = new PropertyGroup();
            propertyGrp.LoadFromString("test,id,employee(id,employeeType(id), lastname))");

            var expected = "employee" + Environment.NewLine +
                           "-employeeType" + Environment.NewLine +
                           "--id" + Environment.NewLine +
                           "-id" + Environment.NewLine +
                           "-lastname" + Environment.NewLine +
                           "id" + Environment.NewLine +
                           "test" + Environment.NewLine;

            Assert.AreEqual(expected, propertyGrp.ConvertToString(true));
        }
    }
}