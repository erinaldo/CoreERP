using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PS.Lib
{
    public class PSMemoryManager
    {
        //private static object _lock = new object();

        //[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        //private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        static PSMemoryManager() { }

        public static void ReleaseUnusedMemory(/*bool runGC = false*/)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //if (runGC)
            //{
            //    // GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            //    GC.Collect(GC.MaxGeneration, GCCollectionMode.Optimized);
            //    // GC.WaitForPendingFinalizers();
            //}
            //try
            //{
            //    lock (PSMemoryManager._lock)
            //    {
            //        PSMemoryManager.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    System.Console.WriteLine(ex.Message);
            //}
        }
    }
}
