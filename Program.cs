using Compl;
using System;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;

class Program
{
//    [SupportedOSPlatform("windows")]
    static unsafe void Main(string[] args)
    {
        // create completion port
        var completionPortHandle = Interop.CreateIoCompletionPort(new IntPtr(-1), IntPtr.Zero, 0, 0);

        var completionPortThread = new Thread(() => new IOCompletionWorker().Start(completionPortHandle))
        {
            IsBackground = true
        };
        completionPortThread.Start();

        const uint Flags = 128 | (uint)1 << 30;

        var fileHandle = Interop.CreateFile("test.txt", (uint)1 << 31, 0, IntPtr.Zero, 3,
            /*FILE_ATTRIBUTE_NORMAL | FILE_FLAG_OVERLAPPED */ Flags,
            IntPtr.Zero);

        Interop.CreateIoCompletionPort(fileHandle,
                                        completionPortHandle,
                                        (uint)fileHandle.ToInt64(),
                                        0);
        if( Interop.GetLastError() != 0 )
        {

        }


        var readBuffer = new byte[1024];
        uint bytesRead;
        var overlapped = new Overlapped()
        {
            AsyncResult = new FileReadAsyncResult()
            {
                ReadCallback = (bytesCount, buffer) =>
                {
                    var contentRead = Encoding.UTF8.GetString(buffer, 0, (int)bytesCount);
                    Console.WriteLine(contentRead);
                },
                Buffer = readBuffer
            }
        };

        NativeOverlapped* nativeOverlapped = overlapped.UnsafePack(null, readBuffer);
        Interop.ReadFile(fileHandle, readBuffer, (uint)readBuffer.Length, out bytesRead, nativeOverlapped);

        //string res = Encoding.UTF8.GetString(readBuffer, 0, (int)bytesRead);
        //Console.WriteLine(res);

        Console.ReadLine();
    }
}