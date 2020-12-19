using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventOfCodeDay09
{
    /**
     * 
     * --- Day 9: Encoding Error ---
     * With your neighbor happily enjoying their video game, you turn your attention to an open data port on
     * the little screen in the seat in front of you.
     * 
     * Though the port is non-standard, you manage to connect it to your computer through the clever use of 
     * several paperclips. Upon connection, the port outputs a series of numbers (your puzzle input).
     * 
     * The data appears to be encrypted with the eXchange-Masking Addition System (XMAS) which, conveniently
     * for you, is an old cypher with an important weakness.
     * 
     * XMAS starts by transmitting a preamble of 25 numbers. After that, each number you receive should be 
     * the sum of any two of the 25 immediately previous numbers. The two numbers will have different values,
     * and there might be more than one such pair.
     * 
     * For example, suppose your preamble consists of the numbers 1 through 25 in a random order. To be valid,
     * the next number must be the sum of two of those numbers:
     * 
     * 26 would be a valid next number, as it could be 1 plus 25 (or many other pairs, like 2 and 24).
     * 49 would be a valid next number, as it is the sum of 24 and 25.
     * 100 would not be valid; no two of the previous 25 numbers sum to 100.
     * 50 would also not be valid; although 25 appears in the previous 25 numbers, the two numbers in the pair
     * must be different.
     * Suppose the 26th number is 45, and the first number (no longer an option, as it is more than 25 numbers
     * ago) was 20. Now, for the next number to be valid, there needs to be some pair of numbers among 1-19,
     * 21-25, or 45 that add up to it:
     * 
     * 26 would still be a valid next number, as 1 and 25 are still within the previous 25 numbers.
     * 65 would not be valid, as no two of the available numbers sum to it.
     * 64 and 66 would both be valid, as they are the result of 19+45 and 21+45 respectively.
     * Here is a larger example which only considers the previous 5 numbers (and has a preamble of length 5):
     * 
     * 35
     * 20
     * 15
     * 25
     * 47
     * 40
     * 62
     * 55
     * 65
     * 95
     * 102
     * 117
     * 150
     * 182
     * 127
     * 219
     * 299
     * 277
     * 309
     * 576
     * 
     * In this example, after the 5-number preamble, almost every number is the sum of two of the previous 5
     * numbers; the only number that does not follow this rule is 127.
     * 
     * The first step of attacking the weakness in the XMAS data is to find the first number in the list (after
     * the preamble) which is not the sum of two of the 25 numbers before it. What is the first number that does
     * not have this property?
     *
     */
    class ProgramDay09
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Starting.");

            string[] fileList = { "data/LiveFile.txt" };
            var preambleLines = 25;
            foreach (var file in fileList)
            {
                ProgramDay09 day09 = new ProgramDay09(file, preambleLines, true);
                Console.WriteLine($"{file} results {day09.oddNumberOut}.");
            }

            //ProgramDay09 day09Test = new ProgramDay09("data/TestFile.txt", 5, true);
            //Console.WriteLine($"data/TestFile.txt results {day09Test.oddNumberOut}.");


            Console.WriteLine($"Finished.");
        }
        private bool verbose = false;
        public Int64 oddNumberOut { get; set; } = 0;
        public int preambleLines { get; set; } = 25;

        public ProgramDay09(string file, int preambleLines = 25, bool verbose = false)
        {
            this.verbose = verbose;
            this.preambleLines = preambleLines;
            List<Int64> dataSet = init(file);
            findFirstOddNumberOut(dataSet);
            List<Int64> sumDataItems = findRangeThatSumsToOddNumber(dataSet);

            foreach (var item in sumDataItems)
            {
                Console.WriteLine($"---{item}");
            }
            Console.WriteLine($" sum of max and min { sumDataItems.Max() + sumDataItems.Min()}");
        }

        private List<Int64> findRangeThatSumsToOddNumber(List<Int64> dataSet)
        {
            List<Int64> returnValues = new List<Int64>();
            var foundList = false;
            var startAt = 0;
            var endAt = 0;
            while (!foundList)
            {
                Int64 sum = 0;
                for (var i = startAt; i < dataSet.Count; i++)
                {
                    sum += dataSet[i];
                    if (sum == oddNumberOut)
                    {
                        foundList = true;
                        endAt = i;
                        break;
                    }
                }
                if (foundList) break;
                startAt++;
            }
            for (var i = startAt; i <= endAt; i++)
            {
                returnValues.Add(dataSet[i]);
            }



            return returnValues;
        }

        private void findFirstOddNumberOut(List<Int64> dataSet)
        {
            var bottom = 0;
            var keepChecking = true;
            for (var i = preambleLines; i < dataSet.Count; i++)
            {
                var numToCheck = dataSet[i];
                var firstNumToAdd = bottom;

                var isValid = false;
                while (firstNumToAdd < i)
                {
                    for (var j = firstNumToAdd + 1; j < i; j++)
                    {
                        var sum = dataSet[firstNumToAdd] + dataSet[j];
                        if (sum == numToCheck)
                        {
                            isValid = true;
                            break;
                        }
                    }
                    if (isValid) break;
                    firstNumToAdd++;
                }
                if (isValid)
                {
                    bottom++;
                }
                else
                {
                    this.oddNumberOut = numToCheck;
                    break;
                }
            }
        }

        private List<Int64> init(string fileName)
        {
            if (this.verbose) Console.WriteLine($"Working with file: {fileName}");
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            string[] lines = File.ReadAllLines(path);
            List<Int64> dataSet = new List<Int64>();
            foreach (var line in lines)
            {
                if (this.verbose) Console.WriteLine($"{line}");
                dataSet.Add(Int64.Parse(line));
            }
            return dataSet;
        }
    }
}
