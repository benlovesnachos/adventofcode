using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AdventOfCodeDay06
{
    class ProgramDay06
    {
        static void Main(string[] args)
        {

            string[] fileList = { "data/TestFile.txt" , "data/LiveFile.txt" };

            foreach (var file in fileList)
            {
                ProgramDay06 pgm = new ProgramDay06(file, false);
                Console.WriteLine($"{file} {pgm.yesSum}");
            }
        }


        public string fileName { get; set; } = "";
        public int yesSum { get; set; } = 0;

        public ProgramDay06(string fileNameIn, bool verbose = false)
        {
            this.fileName = fileNameIn;
            if (verbose) Console.WriteLine($"Working with file: {fileName}");
            doWorkPartTwo(verbose);
        }


        public void doWork(bool verbose = false)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            string[] lines = File.ReadAllLines(path);

            var groupData = "";
            foreach (var line in lines)
            {
                if (String.IsNullOrWhiteSpace(line))
                {
                    int yesCount = processGroupData(groupData);
                    if (verbose) Console.WriteLine($"Count: {yesCount}  Group Data: {groupData} ");
                    if (verbose) Console.WriteLine("-------------------\n");
                    yesSum += yesCount;
                    groupData = "";
                }
                else
                {
                    groupData += line.Trim();
                }
            }

            if (!String.IsNullOrWhiteSpace(groupData))
            {
                int yesCount = processGroupData(groupData);
                if (verbose) Console.WriteLine($"Count: {yesCount}  Group Data: {groupData} ");
                if (verbose) Console.WriteLine("-------------------\n");
                yesSum += yesCount;
                groupData = "";
            }
            if (verbose) Console.WriteLine($"Yes Sum: {yesSum}");
        }

        public void doWorkPartTwo(bool verbose = false)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            string[] lines = File.ReadAllLines(path);

            List<char> groupData = new List<char>();
            bool startNewGroup = true;
            foreach (var line in lines)
            {
                if (String.IsNullOrWhiteSpace(line))
                {
                    if (verbose) Console.WriteLine($"Count: {groupData.Count} ");
                    for (var i = 0; i < groupData.Count; i++) {
                        if (verbose) Console.Write($"{groupData[i]}");
                    }
                    if (verbose) Console.WriteLine("-------------------\n");
                    yesSum += groupData.Count;
                    groupData.Clear();
                    startNewGroup = true;
                }
                else
                {
                    var _line = line.Trim();
                    if (startNewGroup) {
                        //Add to groupData
                        for (var i = 0; i < _line.Length; i++) {
                            groupData.Add(_line[i]);
                        }
                        startNewGroup = false;
                    } else {
                        List<char> _groupData = new List<char>();
                        // if new data of line exists in groupData, add to new _groupData
                        for (var i = 0; i < _line.Length; i++)
                        {
                            if (groupData.Contains(_line[i])) { 
                            _groupData.Add(_line[i]);
                            }
                        }

                        // replace groupData with _groupData
                        groupData =  new List<char>(_groupData);
                    }

                }
            }

            if (verbose) Console.WriteLine($"Count: {groupData.Count}");
            for (var i = 0; i < groupData.Count; i++)
            {
                if (verbose) Console.Write($"{groupData[i]}");
            }
            if (verbose) Console.WriteLine("-------------------\n");
            yesSum += groupData.Count;

            if (verbose) Console.WriteLine($"Yes Sum: {yesSum}");
        }

        private int processGroupData(string groupData)
        {
            List<char> yesAnswers = new List<char>();
            int yesCount = 0;
            for (var i = 0; i < groupData.Length; i++)
            {
                if (!yesAnswers.Contains(groupData[i]))
                {
                    yesAnswers.Add(groupData[i]);
                    yesCount++;
                }
            }
            return yesCount;
        }
    }
}
