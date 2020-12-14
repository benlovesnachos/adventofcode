using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.WindowsRuntime;

namespace AdventOfCodeDay07
{
    class ProgramDay07
    {

        static void Main(string[] args)
        {
            Console.WriteLine($"Starting.");

            string[] fileList = { "data/TestFile2.txt" , "data/LiveFile.txt" };
            string bagToHold = "shiny gold";
            foreach (var file in fileList)
            {
                ProgramDay07 day07 = new ProgramDay07(file, false);
                Console.WriteLine($"{file} can hold {day07.outerThatCanContain(bagToHold)} {bagToHold} bags");
                Console.WriteLine($"{file} will need {day07.mustHaveThisManyInnerBags(bagToHold)} bags inside of a {bagToHold} bag");
            }

            Console.WriteLine($"Finished.");
        }
        private string fileName { get; } = string.Empty;
        private List<BagRule> bagRules { get; } = new List<BagRule>();

        public ProgramDay07(string file, bool verbose = false)
        {
            this.fileName = file;
            if (verbose) Console.WriteLine($"Working with file: {fileName}");
            doWork(verbose);
        }

        public void doWork(bool verbose = false)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            string[] lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                BagRule bagRule = new BagRule(line, verbose);
                if (verbose) Console.WriteLine($"{bagRule}");
                bagRules.Add(bagRule);
            }
        }

        public int outerThatCanContain(string color)
        {
            int listSize = -1;
            bool firstCheck = true;
            List<string> outerColors = new List<string>();
            while (listSize != outerColors.Count)
            {
                listSize = outerColors.Count;
                if (firstCheck)
                {
                    foreach (var bagRule in bagRules)
                    {
                        if (bagRule.canHoldColor(color))
                            outerColors.Add(bagRule.bagColor);
                    }
                    firstCheck = false;
                }
                else
                {
                    List<string> moreToAdd = new List<string>();

                    foreach (var outerColor in outerColors)
                    {
                        foreach (var bagRule in bagRules)
                        {
                            if (bagRule.canHoldColor(outerColor) && !outerColors.Contains(bagRule.bagColor) && !moreToAdd.Contains(bagRule.bagColor))
                                moreToAdd.Add(bagRule.bagColor);
                        }
                    }

                    outerColors.AddRange(moreToAdd);
                }
            }

            return outerColors.Count;
        }

        public int mustHaveThisManyInnerBags(string color, string prependDebug = "")
        {

            Console.WriteLine($"{prependDebug}A ({color}) bag will need...");

            int returnValue = 0;

            var bagRule = bagRules.Where((br) => br.bagColor == color).FirstOrDefault();

            if (bagRule.bagContents.Count == 0) {
                Console.WriteLine($"{prependDebug}... nothing.");
            }

            foreach (var bc in bagRule.bagContents) {
                Console.WriteLine($"{prependDebug}{bc.qty} of {bc.color}");
                returnValue += bc.qty;
                int _inner = mustHaveThisManyInnerBags(bc.color,$"{prependDebug}    ");
                Console.WriteLine($"{prependDebug}+ {bc.color} {bc.qty}*{_inner}");
                returnValue += (bc.qty * _inner);
            }

            return returnValue;
        }


    }

    class BagRule
    {
        public string bagColor { get; } = string.Empty;
        public List<BagContent> bagContents { get; } = new List<BagContent>();

        public BagRule(string ruleLine, bool verbose = false)
        {

            var rules = ruleLine.Split("bags contain");
            bagColor = rules[0].Trim();
            var bagRules = rules[1].Replace(".", "").Replace("bags", "").Replace("bag", "").Trim();

            var bagContains = bagRules.Split(",");
            foreach (var bagContain in bagContains)
            {
                var _bagContain = bagContain.Trim();
                if ("no other" != _bagContain)
                {
                    BagContent bc = new BagContent(_bagContain);
                    bagContents.Add(bc);
                }
            }
            if (verbose)
            {
                Console.WriteLine($"{bagColor}");
                foreach (var bc in bagContents)
                {
                    Console.WriteLine($"\t>>{bc.qty}<< >>{bc.color}<<");
                }
                Console.WriteLine();
            }
        }

        public bool canHoldColor(string aColor)
        {
            foreach (var bagContent in bagContents)
            {
                if (bagContent.color == aColor) return true;
            }
            return false;
        }

        override
        public string ToString()
        {
            string returnValue = bagColor;
            foreach (var bagContent in bagContents)
            {
                returnValue += $"{bagContent.qty} of {bagContent.color},";
            }
            if (returnValue.EndsWith(","))
                returnValue = returnValue.Substring(0, returnValue.Length - 1);
            return returnValue;
        }

    }

    class BagContent
    {
        public int qty { get; set; } = 0;
        public string color { get; set; } = string.Empty;
        public BagContent(string bagContent)
        {
            qty = int.Parse(bagContent.Substring(0, bagContent.IndexOf(" ")).Trim());
            color = bagContent.Substring(bagContent.IndexOf(" ")).Trim();
        }

        override
        public string ToString()
        {
            return $"{qty} {color}";
        }
    }
}
