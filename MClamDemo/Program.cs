using System;
using System.Collections.Generic;
using System.Text;
using MClam;
using MClam.Database;
using MClam.Engine;

namespace MClamDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Always wrap anything that could possibly throw an exception.
            try
            {
                Console.WriteLine("MClam scan file example.");
                Console.WriteLine("========================");
                Console.WriteLine();

                Helpers.InitializeClamAV();

                //Use "using" to avoid memory leak when exception is thrown.
                Console.Write("Creating new engine...");
                using (var cl_engine = ClamEngine.CreateNew())
                {
                    Console.WriteLine("SUCCESS!");

                    //Load database files from directory.
                    Console.Write("Loading CVD into engine...");
                    cl_engine.LoadCvdDirectory(@"D:\MClam\CVD");
                    Console.WriteLine("SUCCESS!");

                    //Compile engine.
                    Console.Write("Compiling engine...");
                    cl_engine.Compile();
                    Console.WriteLine("SUCCESS!");

                    //Open a file to scan.
                    Console.Write("Opening a file...");
                    var fileToScan = FileEntry.Open(@"D:\MClam\eicar.com");
                    Console.WriteLine("SUCCESS!");

                    //Scan the file.
                    Console.Write("Scanning file...");
                    var result = cl_engine.ScanFile(fileToScan);
                    Console.WriteLine("COMPLETED!");
                    Console.WriteLine();

                    //Show file scan info.
                    Console.Write("FullPath: ");
                    Console.WriteLine(result.FullPath);
                    Console.Write("IsVirus: ");
                    Console.WriteLine(result.IsVirus);
                    Console.Write("Scanned: ");
                    Console.WriteLine(result.Scanned);
                    Console.Write("VirusName: ");
                    Console.WriteLine(result.VirusName);
                    Console.WriteLine();

                    //Completed!
                    Console.WriteLine("Scan has been completed!");
                }
            }
            catch (Exception ex)
            {
                //We've got an exception. Write it on screen.
                Console.WriteLine(ex.ToString());
            }
            //Pause here...
            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
