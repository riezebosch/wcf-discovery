using System;
using System.Diagnostics;

namespace WcfDemo.Service.Tests
{
    public class LocalDbHelper
    {
        /// <summary>
        /// http://stackoverflow.com/a/22791484
        /// </summary>
        public static void Delete(string instance = "MSSQLLocalDB")
        {
            var start = new ProcessStartInfo("sqllocaldb", $"stop {instance}");
            start.WindowStyle = ProcessWindowStyle.Hidden;
            using (var stop = Process.Start(start))
                stop.WaitForExit();
            start.Arguments = $"delete {instance}";
            using (var delete = Process.Start(start))
                delete.WaitForExit();
        }
    }
}