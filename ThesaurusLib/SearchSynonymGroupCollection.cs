using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ric.ThesaurusLib
{
    internal class SearchSynonymGroupCollection
    {
        private List<SearchSynonymGroupItem> collection;
        public SearchSynonymGroupCollection()
        {
            collection = new List<SearchSynonymGroupItem>();
        }
        public void Add(bool flag, string synonymGroup)
        {
            collection.Add(new SearchSynonymGroupItem(flag, synonymGroup));
        }
        public IEnumerable<string> GetApplicableSynonymGroups()
        {
            return collection.FindAll(item => item.Flag == true).Select(item => item.SynonymGroup);
        }
        public class SearchSynonymGroupItem
        {
            public bool Flag { get; private set; }
            public string SynonymGroup { get; private set; }
            public SearchSynonymGroupItem(bool flag, string synonymGroup)
            {
                Flag = flag;
                SynonymGroup = synonymGroup;
            }
        }
    }
}
