﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Compl
{
    internal class Interop
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateIoCompletionPort(
            [In] IntPtr fileHandle,
            [In] IntPtr existingCompletionPort,
            [In] UInt32 completionKey,
            [In] UInt32 numberOfConcurrentThreads);

        [DllImport("kernel32.dll")]
        public static extern UInt32 GetLastError();

        [DllImport("kernel32.dll")]
        public static unsafe extern bool GetQueuedCompletionStatus(
            [In] IntPtr completionPort,
            [Out] out UInt32 ptrBytesTransferred,
            [Out] out UInt32 ptrCompletionKey,
            [Out] NativeOverlapped** lpOverlapped,
            [In] UInt32 dwMilliseconds);
        
        [DllImport("kernel32.dll")]
        public static extern bool PostQueuedCompletionStatus(
            [In] IntPtr completionPort,
            [In] UInt32 bytesTrasferred,
            [In] UInt32 completionKey,
            [In] IntPtr lpOverlapped);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateFile(
            [In] string fileName,
            [In] UInt32 dwDesiredAccess,
            [In] UInt32 dwShareMode,
            [In] IntPtr lpSecurityAttributes,
            [In] UInt32 dwCreationDisposition,
            [In] UInt32 dwFlagsAndAttributes,
            [In] IntPtr hTemplateFile);

        [DllImport("kernel32.dll")]
        public static unsafe extern bool ReadFile(
            [In] IntPtr hFile,
            [Out] byte[] lpBuffer,
            [In] uint maxBytesToRead,
            [Out] out UInt32 bytesActuallyRead,
            [In] NativeOverlapped* lpOverlapped);
    }
}
