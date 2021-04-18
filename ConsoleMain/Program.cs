using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleMain
{
    class Program
    {
        private static string _reportFontsFolderName = "ReportFonts";

        static async Task Main(string[] args)
        {
            //var privateFontCollection = new PrivateFontCollection();
            //string webRootPath = @"D:\C# Advanced\DotNetCoreHelper\ConsoleMain";

            //// get all font in directory ReportFont
            //string pathToFontFolder = Path.Combine(webRootPath, _reportFontsFolderName);
            //var filePaths = Directory.GetFiles(pathToFontFolder);
            //foreach (var filePath in filePaths)
            //{
            //    privateFontCollection.AddFontFile(filePath);
            //}
            var t1 = TaskWithAsync();
            //await t1;
            //Console.WriteLine("after wait 5 seconds");

            var t2 = TaskWithAsync2();
            //await t2;
            //Console.WriteLine("after wait 5 seconds 2");

            await Task.WhenAll(t1, t2);
            Console.WriteLine("done all {0} {1}", t1.Result[0], t2.Result[0]);

            Console.ReadKey();
        }

        public static async Task<List<int>> TaskWithAsync()
        {
            Console.WriteLine("task 1 start");
            await Task.Delay(2000);

            return await Task.FromResult(new List<int>() { 1 });
        }

        public static async Task<Dictionary<int ,int>> TaskWithAsync2()
        {
            Console.WriteLine("task 2 start");
            await Task .Delay(3000);

            return await Task .FromResult(new Dictionary<int, int>() { [0] = 2 });
        }
    }
}
