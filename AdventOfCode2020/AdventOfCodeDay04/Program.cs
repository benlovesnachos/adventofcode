using System;
using System.Dynamic;
using System.IO;
using System.Reflection;

namespace AdventOfCodeDay04
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            string[] fileList = { "TestFile.txt", "TestFile2.txt", "LiveFile.txt" };

            foreach (var file in fileList)
            {
                Program pgm = new Program(file);
                Console.WriteLine("{0} has {1} valid passports.", pgm.fileName, pgm.validCount);
            }
        }

        public string fileName { get; set; } = "";
        public int validCount { get; set; } = 0;

        public Program(string fileName)
        {
            this.fileName = fileName;
            doWork();
        }

        public void doWork(bool verbose = false)
        {
            if (verbose) Console.WriteLine("Working with file: {0}", fileName);
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            string[] lines = File.ReadAllLines(path);

            var index = 0;
            var passportData = "";
            foreach (var line in lines)
            {
                if (verbose) Console.WriteLine("{0}\t{1}", ++index, line);
                if (String.IsNullOrWhiteSpace(line))
                {
                    if (verbose) Console.WriteLine("Process Passport: {0}", passportData);
                    Passport p = new Passport(passportData);
                    if (verbose) Console.WriteLine("{0} {1}", p.isValidStrict(), p);
                    if (verbose) Console.WriteLine("-------------------\n");
                    if (p.isValidStrict()) validCount++;
                    passportData = "";
                }
                else
                {
                    passportData += (line.Trim() + " ");
                }
            }

            if (!String.IsNullOrWhiteSpace(passportData))
            {
                if (verbose) Console.WriteLine("Process Passport: {0}", passportData);
                Passport p = new Passport(passportData);
                if (verbose) Console.WriteLine("{0} {1}", p.isValidStrict(), p);
                if (p.isValidStrict()) validCount++;
            }
        }
    }
}
