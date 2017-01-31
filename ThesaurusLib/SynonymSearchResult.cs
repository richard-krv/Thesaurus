using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ric.ThesaurusLib
{
    internal class SynonymSearchResult: ISynonymSearchResult
    {
        public List<IEnumerable<string>> SynonymGroups { get; private set; }

        public IEnumerable<string> SynonymGroups_UnionAll
        {
            get
            {
                var res = new List<string>();
                foreach(var synGr in SynonymGroups)
                {
                    res.AddRange(synGr);
                }
                return res;
            }
        }

        public IEnumerable<string> SynonymGroups_Union
        {
            get
            {
                var res = new HashSet<string>();
                foreach(var synGr in SynonymGroups)
                {
                    res.UnionWith(new HashSet<string>(synGr));
                }
                return res;
            }
        }

        public IEnumerable<string> SynonymGroups_Intersect
        {
            get
            {
                var res = new List<string>();
                foreach (var synGr in SynonymGroups)
                {
                    res.Intersect(synGr);
                }
                return res;
            }
        }

        public SynonymSearchResult()
        {
            SynonymGroups = new List<IEnumerable<string>>();
        }

        public void AddSynonymGroupData(IEnumerable<string> synonyms)
        {
            SynonymGroups.Add(synonyms);
        }
    }
}
