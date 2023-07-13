using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Compl
{
    internal class FileReadAsyncResult : IAsyncResult
    {
        public object? AsyncState { get; private set; }

        public WaitHandle AsyncWaitHandle { get; private set; }

        public bool CompletedSynchronously { get; private set; }

        public bool IsCompleted { get; private set; }

        public Action<uint, byte[]> ReadCallback { get; set; }

        public byte[] Buffer { get; set; }
    }
}
