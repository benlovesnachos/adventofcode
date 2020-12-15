using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeDay08
{
    class StartupRule
    {
        public string operation { get; set; } = string.Empty;
        public int argument { get; set; } = 0;
        private bool verbose = false;
        private StartupRule() { }
        public StartupRule(string ruleLine, bool verbose = false)
        {
            this.verbose = verbose;
            var elems = ruleLine.Trim().Split(" ");
            operation = elems[0];
            argument = int.Parse(elems[1]);
        }

        override
        public string ToString()
        {
            return $"{operation} {argument}";
        }


        public StartupRule copy()
        {
            StartupRule sr = new StartupRule()
            {
                verbose = this.verbose,
                operation = this.operation,
                argument = this.argument,
            };
            return sr;
        }
    }
}
