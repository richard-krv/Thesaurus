using Ric.ThesaurusLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ric.Thesaurus.ConsoleThesaurusClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = ThesaurusFactory.GetThesaurus(ThesaurusPersistance.File, ThesaurusMultiResult.Union, 300000);
            t.AddSynonyms(ThesaurusTestSynonyms.GetSynonyms1());
            t.AddSynonyms(ThesaurusTestSynonyms.GetSynonyms2());
            Console.WriteLine("Synonyms saved successfully. Press any key...");
            Console.ReadKey();
            var word = "syn_1_a";
            var synonyms = t.GetSynonyms(word);
            Console.WriteLine("Synonyms of {0} are \n{1}. \nPress any key...", word, string.Join(",\n", synonyms));
            Console.ReadKey();
        }

    }
}
