using Ric.ThesaurusLib.Repositories;
using Thesaurus;

namespace Ric.ThesaurusLib
{
    public enum ThesaurusPersistance
    {
        File,
        SqlServer,
        Redis,
    }
    public enum ThesaurusMultiResult
    {
        Throw,
        Union,
        UnionAll,
        Intersect
    }
    public class ThesaurusFactory
    {
        public static IThesaurus GetThesaurus(ThesaurusPersistance thesPersistance, ThesaurusMultiResult multires,
            int timeoutMilliseconds)
        {
            IThesaurusRepository repository = null;
            switch (thesPersistance)
            {
                case ThesaurusPersistance.File:
                    repository = new ThesaurusRepositoryFile(timeoutMilliseconds); break;
                case ThesaurusPersistance.SqlServer:
                    repository = new ThesaurusRepositorySqlServer(timeoutMilliseconds); 
                    break;
                case ThesaurusPersistance.Redis:
                    repository = null; break;
            };

            return GetThesaurus(repository, multires);
        }

        public static IThesaurus GetThesaurus(IThesaurusRepository repository, ThesaurusMultiResult multires)
        {
            return new Thesaurus(repository, multires);
        }
    }
}
