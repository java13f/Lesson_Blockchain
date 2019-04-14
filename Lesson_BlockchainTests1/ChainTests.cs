using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lesson_Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_Blockchain.Tests
{
    [TestClass()]
    public class ChainTests
    {
        [TestMethod()]
        public void ChainTest()
        {
            var chain = new Chain();
            chain.Add("Ура ура ура", "Дениска");
                        
            Assert.AreEqual("Дениска", chain.Last.User);
        }

        [TestMethod()]
        public void CheckTest()
        {
            var chain = new Chain();
            chain.Add("hello Word", "User");
            chain.Add("Code Blog", "Vadim");

            Assert.IsTrue(chain.Check());
        }
    }
}