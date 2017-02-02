using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ric.ThesaurusLib.Repositories
{
    public interface IThesaurusSynonymGroupRepository
    {
        Task<bool> IsContainedInSynonymGroupAsync(string word, string synonymGroupName);
        IEnumerable<string> GetSynonymGroupNames();
        string GetNewSynonymGroupName();
        void SaveToPersistence(string synonymsGroupName, IEnumerable<string> synonyms);
        IEnumerable<string> LoadSynonymGroupWords(string synonymGroupName);
    }
}
