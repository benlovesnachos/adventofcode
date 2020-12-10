using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeDay02
{
    class PasswordLine
    {

        public int min { get; set; } = 0;
        public int max { get; set; } = 0;
        public string character { get; set; } = String.Empty;
        public string password { get; set; } = String.Empty;

        public PasswordLine()
        {
        }

        public PasswordLine(string input)
        {
            var parts = input.Split(" ");

            var minMax = parts[0].Split("-");
            min = Int32.Parse(minMax[0]);
            max = Int32.Parse(minMax[1]);

            character = parts[1].Replace(":", "");
            password = parts[2];
            Console.WriteLine("min <{0}> max <{1}>   character <{2}>   password <{3}>", min, max, character, password);
        }

        public bool isValid()
        {
            var count = 0;
            for (int i = 0; i < password.Length; i++)
            {
                Console.WriteLine("Compare {0} and {1}", password[i].ToString(), character);

                if (password[i].ToString() == character) count++;
            }
            if (count >= min && count <= max) return true;

            return false;
        }

        /**
         * --- Part Two ---
         * While it appears you validated the passwords correctly, they don't seem to be what the Official Toboggan Corporate Authentication System is expecting.
         * 
         * The shopkeeper suddenly realizes that he just accidentally explained the password policy rules from his old job at the sled rental place down the street! The Official Toboggan Corporate Policy actually works a little differently.
         * 
         * Each policy actually describes two positions in the password, where 1 means the first character, 2 means the second character, and so on. (Be careful; Toboggan Corporate Policies have no concept of "index zero"!) Exactly one of these positions must contain the given letter. Other occurrences of the letter are irrelevant for the purposes of policy enforcement.
         * 
         * Given the same example list from above:
         * 
         * 1-3 a: abcde is valid: position 1 contains a and position 3 does not.
         * 1-3 b: cdefg is invalid: neither position 1 nor position 3 contains b.
         * 2-9 c: ccccccccc is invalid: both position 2 and position 9 contain c.
         * 
         * How many passwords are valid according to the new interpretation of the policies?
         */
        public bool isValidPartTwo()
        {
            var returnValue = false;

            if ((password[min - 1].ToString() == character) && !(password[max - 1].ToString() == character)) returnValue = true;

            if (!(password[min - 1].ToString() == character) && (password[max - 1].ToString() == character)) returnValue = true;

            return returnValue;
        }
    }
}
