using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Compl
{
    public class IOCompletionWorker
    {
        public unsafe void Start(IntPtr completionPort)
        {
            while (true)
            {
                uint bytesRead;
                uint completionKey;
                NativeOverlapped* nativeOverlapped;

                var result = Interop.GetQueuedCompletionStatus(
                    completionPort,
                    out bytesRead,
                    out completionKey,
                    &nativeOverlapped,
                    uint.MaxValue);

                var overlapped = Overlapped.Unpack(nativeOverlapped);

                if (result)
                {
                    var asyncResult = ((FileReadAsyncResult)overlapped.AsyncResult);
                    asyncResult.ReadCallback(bytesRead, asyncResult.Buffer);
                }
                else
                {
                    Console.WriteLine(Interop.GetLastError().ToString());
                }

                Overlapped.Free(nativeOverlapped);


            }
        }
    }
}
