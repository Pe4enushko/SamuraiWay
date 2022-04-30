using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiDbWork;
using System;

namespace Tests
{
    [TestClass]
    public class DBTest
    {
        static string databaseName = "ayaya.db";
        static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string dbPath = System.IO.Path.Combine(folderPath, databaseName);
        Challenges ch = new Challenges(dbPath);
        [TestMethod]
        public void GetNameTest()
        {
            string response = ch.GetDescription("noSugar");
            Assert.AreEqual("Никакого сахара сегодня", response);
        }
        [TestMethod]
        public void GetChallengeTest()
        {
            Challenge chal = ch.GetChallengeById(4);
            Assert.AreEqual(new Challenge(4, "noSugar", "Никакого сахара сегодня").Name,chal.Name);
        }
    }
}
