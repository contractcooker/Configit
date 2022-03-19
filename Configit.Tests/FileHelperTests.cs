using System;
using AutoBogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Configit.Tests
{
    [TestClass]
    public class UnitTest1
    {
        internal FileHelper _fileHelper;

        [TestInitialize]
        public void Init()
        {
            
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullDependency_ThrowsArgumentNullException()
        {
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyDependency_ThrowsArgumentException()
        {
        }
    }
}