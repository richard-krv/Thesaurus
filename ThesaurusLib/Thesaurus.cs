using Ric.ThesaurusLib.Exceptions;
using Ric.ThesaurusLib.Repositories;
using System.Collections.Generic;
using Thesaurus;

namespace Ric.ThesaurusLib
{
    internal class Thesaurus : IThesaurus
    {
        IThesaurusRepository repository;
        ThesaurusMultiResult multiResultSetting = ThesaurusMultiResult.UnionAll;
        public Thesaurus(IThesaurusRepository repository)
        {
            this.repository = repository;
        }
        public Thesaurus(IThesaurusRepository repository, ThesaurusMultiResult multiResultSetting):
            this(repository)
        {
            this.multiResultSetting = multiResultSetting;
        }
        public void AddSynonyms(IEnumerable<string> synonyms)
        {
            repository.SaveSynonyms(synonyms);
        }

        public IEnumerable<string> GetSynonyms(string word)
        {
            var synGroups = repository.GetSynonyms(word);
            IEnumerable<string> result = null;
            switch (multiResultSetting)
            {
                case ThesaurusMultiResult.UnionAll:
                    result = synGroups.SynonymGroups_UnionAll; break;
                case ThesaurusMultiResult.Union:
                    result = synGroups.SynonymGroups_Union; break;
                case ThesaurusMultiResult.Intersect:
                    result = synGroups.SynonymGroups_Intersect; break;
                case ThesaurusMultiResult.Throw:
                    throw new InvalidSearchResultException(string.Format(
                        "There's multiple synonym result sets for word {0}", word));
            }
            return result;
        }

        public IEnumerable<string> GetWords()
        {
            return repository.GetAllWords();
        }
    }
}
