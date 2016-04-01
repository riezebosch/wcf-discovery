using Discovery.Console.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Discovery.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                try
                {
                    var client = new OracleClient();
                    client
                        .ClientCredentials
                        .UserName
                        .UserName = "discovery";
                    client
                        .ClientCredentials
                        .UserName
                        .Password = "tesla";

                    try
                    {
                        var result = client.Ask("random");

                        ForegroundColor = ConsoleColor.White;
                        Write(result);

                        ForegroundColor = client.InnerChannel.RemoteAddress.Uri.Host == "docenta" ? ConsoleColor.Gray : ConsoleColor.Green;
                        WriteLine($" ({client.InnerChannel.RemoteAddress})");
                    }
                    catch (Exception ex)
                    {
                        WriteError(ex.Message, client);
                    }
                    finally
                    {
                        if (client.State == CommunicationState.Opened)
                        {
                            client.Close();
                        }
                        else
                        {
                            client.Abort();
                        }
                    }
                }
                catch (EndpointNotFoundException)
                {
                    WriteError("Endpoint not found");
                }
               
            }
        }

        private static void WriteError(string error)
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine(error);
        }

        private static void WriteError(string error, OracleClient client)
        {
            WriteError($"{error} from {client.InnerChannel.RemoteAddress.Uri.Host}");
        }
    }
}
