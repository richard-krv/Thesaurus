using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ric.Thesaurus.ConsoleThesaurusClient
{
    public class ThesaurusTestSynonyms
    {
        public static IEnumerable<string> GetSynonyms1()
        {
            return new []
            {
                "syn_1_a",
                "syn_1_b",
                "syn_1_c",
                "syn_1_d",
                "syn_1_e",
                "syn_1_f",
                "syn_1_g",
            };
        }

        public static IEnumerable<string> GetSynonyms2()
        {
            return new []
            {
                "syn_2_a",
                "syn_2_b",
                "syn_2_c",
                "syn_2_d",
                "syn_2_e",
                "syn_2_f",
                "syn_2_g",
            };
        }
    }
}
