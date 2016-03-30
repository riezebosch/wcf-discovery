using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Netflix.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new NetflixServiceReference.NetflixServiceClient();
            var result = client.Top10();

            WriteLine(string.Join(Environment.NewLine, result));
        }
    }
}
