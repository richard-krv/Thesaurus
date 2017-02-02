using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ric.ThesaurusLib.Repositories
{
    internal abstract class RepositoryBase : IThesaurusRepository, IThesaurusSynonymGroupRepository, IMultithreadedCancellable
    {
        internal const int DefaultCancelTimeoutMilliseconds = 60000;
        public RepositoryBase(int cancelTimeoutMilliseconds = DefaultCancelTimeoutMilliseconds)
        {
            CancelSrc = new CancellationTokenSource(cancelTimeoutMilliseconds);
        }

        #region IMultithreadedCancellable
        public CancellationTokenSource CancelSrc { get; private set; }
        #endregion

        #region IThesaurusRepository
        public void SaveSynonyms(IEnumerable<string> synonyms)
        {
            var newSGN = GetNewSynonymGroupName();

            SaveToPersistence(newSGN, synonyms);
        }
        public ISynonymSearchResult GetSynonyms(string word)
        {
            var fileNames = GetSynonymGroupNames();
            var tasks = new List<Task>();
            var results = new SearchSynonymGroupCollection();
            foreach (var fileName in fileNames)
            {
                if (CancelSrc.IsCancellationRequested)
                    break;
                tasks.Add(Task.Run(async () => results.Add(await IsContainedInSynonymGroupAsync(word, fileName), fileName)));
            }
            Task.WaitAll(tasks.ToArray(), CancelSrc.Token);

            var synGrs = results.GetApplicableSynonymGroups();

            var synonymGroupsResult = new SynonymSearchResult();
            foreach (var synGrp in synGrs)
            {
                synonymGroupsResult.AddSynonymGroupData(LoadSynonymGroupWords(synGrp));
            }
            return synonymGroupsResult;
        }
        public IEnumerable<string> GetAllWords()
        {
            var fileNames = GetSynonymGroupNames();
            var synonymGroupsData = new ConcurrentBag<IEnumerable<string>>();
            var tasks = new List<Task>();
            foreach (var file in fileNames)
                tasks.Add(Task.Run(() => synonymGroupsData.Add(LoadSynonymGroupWords(file)), CancelSrc.Token));

            Task.WaitAll(tasks.ToArray());
            var result = new List<string>();
            foreach (var file in synonymGroupsData)
                result.AddRange(file);

            return result.ToArray();
        }
        #endregion

        public abstract Task<bool> IsContainedInSynonymGroupAsync(string word, string synonymGroupName);
        public abstract IEnumerable<string> GetSynonymGroupNames();
        public abstract string GetNewSynonymGroupName();
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
