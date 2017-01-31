using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ric.ThesaurusLib.Repositories
{
    internal abstract class RepositoryBase : IThesaurusRepository
    {
        internal const int DefaultCancelTimeoutMilliseconds = 60000;
        public RepositoryBase(int cancelTimeoutMilliseconds = DefaultCancelTimeoutMilliseconds)
        {
            CancelSrc = new CancellationTokenSource(cancelTimeoutMilliseconds);
        }
        public CancellationTokenSource CancelSrc { get; private set; }

        public abstract void SaveSynonyms(IEnumerable<string> synonyms);
        public abstract ISynonymSearchResult GetSynonyms(string word);
        public abstract IEnumerable<string> GetAllWords();

        public abstract IEnumerable<string> GetSynonymGroupNames();
        public abstract IEnumerable<string> LoadSynonymGroupWords(string synonymGroupName);
        public abstract void SaveToPersistence(string synonymsGroupName, IEnumerable<string> synonyms);

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    CancelSrc.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ThesaurusRepositorySqlServer() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
