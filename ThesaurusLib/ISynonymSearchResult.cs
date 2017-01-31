using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ric.ThesaurusLib
{
    public interface ISynonymSearchResult
    {
        List<IEnumerable<string>> SynonymGroups { get; }
        IEnumerable<string> SynonymGroups_UnionAll { get; }
        IEnumerable<string> SynonymGroups_Union { get; }
        IEnumerable<string> SynonymGroups_Intersect { get; }
    }
}
