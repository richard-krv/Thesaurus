using System.Collections.Generic;

namespace Ric.ThesaurusLib.Repositories
{
    public interface IThesaurusRepository
    {
        void SaveSynonyms(IEnumerable<string> synonyms);
        ISynonymSearchResult GetSynonyms(string word);
        IEnumerable<string> GetAllWords();
    }
}
