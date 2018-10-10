using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ZoneModel.Model;
using ZoneModel.Model.Validation;
using ZoneModel.Services;

namespace NetworkZoneModelCli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = AppServices.Services;

            Console.WriteLine("Wrote sample files");
            Console.ReadKey();
        }
    }
}
