using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ric.ThesaurusLib.Repositories
{
    internal class ThesaurusRepositorySqlServer : RepositoryBase
    {
        public ThesaurusRepositorySqlServer(int cancelTimeoutMilliseconds = 60000) : base(cancelTimeoutMilliseconds)
        {
        }

        public override string GetNewSynonymGroupName()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetSynonymGroupNames()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> IsContainedInSynonymGroupAsync(string word, string synonymGroupName)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> LoadSynonymGroupWords(string synonymGroupName)
        {
            throw new NotImplementedException();
        }

        public override void SaveToPersistence(string synonymsGroupName, IEnumerable<string> synonyms)
        {
            throw new NotImplementedException();
        }
    }
}
