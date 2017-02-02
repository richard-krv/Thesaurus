using System;
using System.Threading;

namespace Ric.ThesaurusLib.Repositories
{
    public interface IMultithreadedCancellable : IDisposable
    {
        CancellationTokenSource CancelSrc { get; }
    }
}
