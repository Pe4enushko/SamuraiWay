using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiDbWork.LocalDB;
using SamuraiDbWork.Models;
using System;

namespace Tests
{
    [TestClass]
    public class DBTest
    {
        static string databaseName = "ayaya.db";
        static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string dbPath = System.IO.Path.Combine(folderPath, databaseName);
        Challenges ch = new Challenges();
        [TestMethod]
        public void GetNameTest()
        {
            string response = ch.GetDescription("noSugar");
            Assert.AreEqual("Никакого сахара сегодня", response);
        }
        [TestMethod]
        public void GetChallengeTest()
        {
            ChallengeModel chal = ch.GetChallengeById(4);
            Assert.AreEqual(new ChallengeModel(4, "noSugar", "Никакого сахара сегодня").Name,chal.Name);
        }
    }
}
