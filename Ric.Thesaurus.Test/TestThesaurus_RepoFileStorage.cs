using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ric.ThesaurusLib;
using System.Linq;
using Moq;
using System.Threading;
using System.Collections.Generic;
using Ric.ThesaurusLib.Repositories;

namespace Ric.Thesaurus.Test
{
    [TestClass]
    public class TestThesaurus_RepoFileStorage
    {
        
        IEnumerable<string> Repo_GetSynonyms(string word)
        {
            switch (word)
            {
                case "syn_1_a": return ThesaurusTestSynonyms.GetSynonyms1();
                case "syn_2_a": return ThesaurusTestSynonyms.GetSynonyms2();
            }
            return null;
        }
        IEnumerable<string> Repo_GetSynonymsGroupData(string groupName)
        {
            switch (groupName)
            {
                case "1": return ThesaurusTestSynonyms.GetSynonyms1();
                case "2": return ThesaurusTestSynonyms.GetSynonyms2(); 
            }
            return null;
        }
        void Repo_SaveToPersistence(string synGrNm, IEnumerable<string> data)
        {
            switch (synGrNm)
            {
                case "3": CollectionAssert.AreEqual(ThesaurusTestSynonyms.GetSynonyms1().ToArray(), data.ToArray()); break;
                case "4": CollectionAssert.AreEqual(ThesaurusTestSynonyms.GetSynonyms2().ToArray(), data.ToArray()); break;
                default: Assert.Fail("New group name has been assigned incorrectly"); break;
            }
            SynGroupNamesList.Add(synGrNm);
        }
        IEnumerable<string> Repo_GetSynonymGroupNames()
        {
            return SynGroupNamesList;
        }
        private List<string> SynGroupNamesList;
        IThesaurusRepository GetRepository()//Repo_TestMethod tm
        {
            SynGroupNamesList = new List<string>() { "1", "2" };
            var repo = new Mock<ThesaurusRepositoryFile>(1000000);
            repo.CallBase = true;
            repo.Setup(r => r.GetSynonymGroupNames()).Returns(() => Repo_GetSynonymGroupNames());
            repo.Setup(r => r.LoadSynonymGroupWords(It.IsAny<string>()))
                .Returns<string>((synGrNm) => Repo_GetSynonymsGroupData(synGrNm));
            repo.Setup(r => r.SaveToPersistence(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()))
                .Callback<string, IEnumerable<string>>((synGrNm, data) => { { Repo_SaveToPersistence(synGrNm, data); } });

            return repo.Object;
        }
        enum Repo_TestMethod
        {
            SaveSynonyms,
            GetSynonyms,
            GetWords
        }
        [TestMethod]
        public void Test_SaveSynonymGroup()
        {
            var thFile = ThesaurusFactory.GetThesaurus(GetRepository(), ThesaurusMultiResult.Union);
            thFile.AddSynonyms(ThesaurusTestSynonyms.GetSynonyms1());
            thFile.AddSynonyms(ThesaurusTestSynonyms.GetSynonyms2());
        }


        [TestMethod]
        public void Test_GetSynonymGroup()
        {
            var thFile = ThesaurusFactory.GetThesaurus(GetRepository(), ThesaurusMultiResult.Union);
            var syns = thFile.GetSynonyms("syn_1_a");
            CollectionAssert.AreEqual(ThesaurusTestSynonyms.GetSynonyms1().ToArray(), syns.ToArray());
            var syns2 = thFile.GetSynonyms("syn_2_a");
            CollectionAssert.AreEqual(ThesaurusTestSynonyms.GetSynonyms2().ToArray(), syns2.ToArray());
        }

        [TestMethod]
        public void Test_GetAllWords()
        {
            var thFile = ThesaurusFactory.GetThesaurus(GetRepository(), ThesaurusMultiResult.Union);
            var syns = thFile.GetWords();
            var expected = ThesaurusTestSynonyms.GetSynonyms1().Union(ThesaurusTestSynonyms.GetSynonyms2());
            foreach(var expectedWord in expected)
            {
                if (!syns.Contains(expectedWord))
                    Assert.Fail(string.Format("The expected word '{0}' is not contained in the resulting set: {1}",
                        expectedWord, string.Join(",", syns)));
            }
        }
    }
}
