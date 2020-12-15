using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AdventOfCodeDay08
{
    class ProgramDay08
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Starting.");

            string[] fileList = { "data/TestFile.txt" , "data/LiveFile.txt" };
            foreach (var file in fileList)
            {
                ProgramDay08 day08 = new ProgramDay08(file, false);
                Console.WriteLine($"Value of accumulator at end of {file} is {day08.accumulator}.");
            }

            Console.WriteLine($"Finished.");
        }
        private bool verbose = false;
        public int accumulator = 0;

        public ProgramDay08(string file, bool verbose = false)
        {
            this.verbose = verbose;
            List<StartupRule> startupRules = init(file);
            var changeInstructionAt = 0;
            var completed = doWork(startupRules);

            while (!completed)
            {
                accumulator = 0;
                var _startupRules = alterList(startupRules, changeInstructionAt);
                completed = doWork(_startupRules);
                changeInstructionAt++;
            }

        }

        public List<StartupRule> init(string fileName)
        {
            if (this.verbose) Console.WriteLine($"Working with file: {fileName}");
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            string[] lines = File.ReadAllLines(path);
            List<StartupRule> startupRules = new List<StartupRule>();
            foreach (var line in lines)
            {
                StartupRule startupRule = new StartupRule(line, this.verbose);
                if (this.verbose) Console.WriteLine($"{startupRule}");
                startupRules.Add(startupRule);
            }
            return startupRules;
        }

        public List<StartupRule> alterList(List<StartupRule> startupRules, int changeAt)
        {
            List<StartupRule> _startupRules = new List<StartupRule>();

            var index = 0;
            foreach (var startupRule in startupRules)
            {
                StartupRule sr = startupRule.copy();

                if (index == changeAt)
                {
                    if (sr.operation == "nop" && sr.argument != 0) { sr.operation = "jmp"; }
                    else if (sr.operation == "jmp") { sr.operation = "nop"; }
                }
                _startupRules.Add(sr);
                index++;
            }

            return _startupRules;
        }

        public bool doWork(List<StartupRule> startupRules)
        {
            /**
             * acc increases or decreases a single global value called the accumulator by the value given in the argument. For example, acc +7 would increase the accumulator by 7.The accumulator starts at 0.After an acc instruction, the instruction immediately below it is executed next.
             * 
             * jmp jumps to a new instruction relative to itself.The next instruction to execute is found using the argument as an offset from the jmp instruction; for example, jmp + 2 would skip the next instruction, jmp + 1 would continue to the instruction immediately below it, and jmp - 20 would cause the instruction 20 lines above to be executed next.
             * 
             * nop stands for No OPeration -it does nothing.The instruction immediately below it is executed next.
             * 
             */
            var ranToCompletion = false;
            int instructionNumber = 0;
            List<int> rulesRan = new List<int>();

            for (var i = 0; i < startupRules.Count; i++)
            {
                if (instructionNumber >= startupRules.Count)
                {
                    if (this.verbose) Console.WriteLine($"Reached end of instructions!!! {instructionNumber}, accumulator is {accumulator}");
                    ranToCompletion = true;
                    break;
                }

                StartupRule sr = startupRules[instructionNumber];
                if (this.verbose) Console.WriteLine($"Running step {i}, instruction {instructionNumber}, rule >>{sr}<<");

                if (rulesRan.Contains(instructionNumber))
                {
                    if (this.verbose) Console.WriteLine($"Already ran instruction {instructionNumber}, accumulator is {accumulator}");
                    ranToCompletion = false;
                    break;
                }
                rulesRan.Add(instructionNumber);

                switch (sr.operation)
                {
                    case "nop":
                        instructionNumber++;
                        break;

                    case "jmp":
                        instructionNumber += sr.argument;
                        break;

                    case "acc":
                        instructionNumber++;
                        accumulator += sr.argument;
                        break;

                    default:
                        break;
                }
            }

            return ranToCompletion;
        }
    }
}
