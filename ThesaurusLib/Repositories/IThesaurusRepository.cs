using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ric.ThesaurusLib.Repositories
{
    public interface IThesaurusRepository : IDisposable
    {
        void SaveSynonyms(IEnumerable<string> synonyms);
        ISynonymSearchResult GetSynonyms(string word);
        IEnumerable<string> GetAllWords();

        IEnumerable<string> GetSynonymGroupNames();
        CancellationTokenSource CancelSrc { get; }
        void SaveToPersistence(string synonymsGroupName, IEnumerable<string> synonyms);
        //string LoadSynonymGroupText(string synonymGroupName);
        IEnumerable<string> LoadSynonymGroupWords(string synonymGroupName);
    }
}
