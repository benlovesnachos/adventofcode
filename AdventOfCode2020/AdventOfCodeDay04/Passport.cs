using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCodeDay04
{


    class Passport
    {
        /**
         *
         * byr (Birth Year)
         * iyr (Issue Year)
         * eyr (Expiration Year)
         * hgt (Height)
         * hcl (Hair Color)
         * ecl (Eye Color)
         * pid (Passport ID)
         * cid (Country ID)
         * 
         */

        public string BirthYear { get; set; }
        public string IssueYear { get; set; }
        public string ExpirationYear { get; set; }
        public string Height { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string PassportID { get; set; }
        public string CountryID { get; set; }
        public bool _verbose { get; set; } = false;

        public Passport(string passportData, bool verbose = false)
        {
            _verbose = verbose;
            parse(passportData);

        }

        private void parse(string passportData)
        {
            var elements = passportData.Split(" ");
            foreach (var element in elements)
            {
                fillData(element);
            }

        }

        private void fillData(string element)
        {
            var pieces = element.Split(":");

            switch (pieces[0])
            {
                case "byr":
                    BirthYear = pieces[1];
                    break;
                case "iyr":
                    IssueYear = pieces[1];
                    break;
                case "eyr":
                    ExpirationYear = pieces[1];
                    break;
                case "hgt":
                    Height = pieces[1];
                    break;
                case "hcl":
                    HairColor = pieces[1];
                    break;
                case "ecl":
                    EyeColor = pieces[1];
                    break;
                case "pid":
                    PassportID = pieces[1];
                    break;
                case "cid":
                    CountryID = pieces[1];
                    break;
                default:
                    break;
            }
        }

        public bool isValid()
        {
            if (String.IsNullOrWhiteSpace(BirthYear)) return false;
            if (String.IsNullOrWhiteSpace(IssueYear)) return false;
            if (String.IsNullOrWhiteSpace(ExpirationYear)) return false;
            if (String.IsNullOrWhiteSpace(Height)) return false;
            if (String.IsNullOrWhiteSpace(HairColor)) return false;
            if (String.IsNullOrWhiteSpace(EyeColor)) return false;
            if (String.IsNullOrWhiteSpace(PassportID)) return false;
            if (String.IsNullOrWhiteSpace(CountryID)) return false;
            return true;
        }

        public bool isKindOfValid()
        {
            if (String.IsNullOrWhiteSpace(BirthYear)) return false;
            if (String.IsNullOrWhiteSpace(IssueYear)) return false;
            if (String.IsNullOrWhiteSpace(ExpirationYear)) return false;
            if (String.IsNullOrWhiteSpace(Height)) return false;
            if (String.IsNullOrWhiteSpace(HairColor)) return false;
            if (String.IsNullOrWhiteSpace(EyeColor)) return false;
            if (String.IsNullOrWhiteSpace(PassportID)) return false;
            //if (String.IsNullOrWhiteSpace(CountryID)) return false;
            return true;
        }


        public bool isValidStrict()
        {
            if (String.IsNullOrWhiteSpace(BirthYear))
            {
                if (_verbose) Console.WriteLine("Fail BirthYear");
                return false;
            }

            var birthYear = Int32.Parse(BirthYear);
            if (birthYear < 1920 || birthYear > 2002)
            {
                if (_verbose) Console.WriteLine("Fail BirthYear range");
                return false;
            }

            if (String.IsNullOrWhiteSpace(IssueYear))
            {
                if (_verbose) Console.WriteLine("Fail IssueYear");
                return false;
            }

            var issueYear = Int32.Parse(IssueYear);
            if (issueYear < 2010 || issueYear > 2020)
            {
                if (_verbose) Console.WriteLine("Fail IssueYear range");
                return false;
            }

            if (String.IsNullOrWhiteSpace(ExpirationYear))
            {
                if (_verbose) Console.WriteLine("Fail ExpirationYear");
                return false;
            }

            var expirationYear = Int32.Parse(ExpirationYear);
            if (expirationYear < 2020 || expirationYear > 2030)
            {
                if (_verbose) Console.WriteLine("Fail ExpirationYear range");
                return false;
            }

            if (String.IsNullOrWhiteSpace(Height))
            {
                if (_verbose) Console.WriteLine("Fail Height");
                return false;
            }

            if (Height.EndsWith("in") || Height.EndsWith("cm"))
            {
                if (Height.EndsWith("in"))
                {
                    var height = Int32.Parse(Height.Replace("in", ""));
                    if (height < 59 || height > 76)
                    {
                        if (_verbose) Console.WriteLine("Fail Height in range");
                        return false;
                    }

                }
                if (Height.EndsWith("cm"))
                {
                    var height = Int32.Parse(Height.Replace("cm", ""));
                    if (height < 150 || height > 193)
                    {
                        if (_verbose) Console.WriteLine("Fail Height cm range");
                        return false;
                    }
                }
            }
            else
            {
                if (_verbose) Console.WriteLine("Fail Height in/cm");
                return false;
            }

            if (String.IsNullOrWhiteSpace(HairColor))
            {
                if (_verbose) Console.WriteLine("Fail HairColor");
                return false;
            }


            Regex rgxHairColor = new Regex(@"^\#[a-f0-9]{6}");
            if (!rgxHairColor.IsMatch(HairColor))
            {
                if (_verbose) Console.WriteLine("Fail HairColor range");
                return false;
            }


            if (String.IsNullOrWhiteSpace(EyeColor))
            {
                if (_verbose) Console.WriteLine("Fail EyeColor");
                return false;
            }

            switch (EyeColor)
            {
                case "amb":
                case "blu":
                case "brn":
                case "gry":
                case "grn":
                case "hzl":
                case "oth":

                    break;
                default:
                    if (_verbose) Console.WriteLine("Fail EyeColor check");
                    return false;
            }



            if (String.IsNullOrWhiteSpace(PassportID))
            {
                if (_verbose) Console.WriteLine("Fail PassportId");
                return false;
            }


            Regex rgxPassportID = new Regex(@"^[0-9]{9}$");
            if (!rgxPassportID.IsMatch(PassportID))
            {
                if (_verbose) Console.WriteLine("Fail PassportID regex");
                return false;
            }

            if (String.IsNullOrWhiteSpace(CountryID))
            {
                if (_verbose) Console.WriteLine("Fail CountryID... not really ;)");
                //return false;
            }

            return true;
        }

        override
        public string ToString()
        {

            return $"byr:{BirthYear} " +
                $"iyr:{IssueYear} " +
                $"eyr:{ExpirationYear} " +
                $"hgt:{Height} " +
                $"hcl:{HairColor} " +
                $"ecl:{EyeColor} " +
                $"pid:{PassportID} " +
                $"cid:{CountryID}";

        }
    }
}
