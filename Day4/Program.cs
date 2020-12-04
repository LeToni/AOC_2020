using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = "./Input.txt";
            var passports = ProcessInput(file);
            var countValidPassports = 0;

            countValidPassports = passports.Where(FirstPassportPolicy)
                                            .Select(p => p)
                                            .Count();
            Console.WriteLine($"Number of valid password according to first password policy: {countValidPassports}");

            countValidPassports = passports.Where(checkPIDPolicy)
                .Where(checkECLPolicy)
                .Where(checkHCLPolicy)
                .Where(checkHGTPolicy)
                .Where(checkEYRPolicy)
                .Where(checkIYRPolicy)
                .Where(checkBYRPolicy)
                .Select(p => p).Count();

            Console.WriteLine($"Number of valid password according to first password policy: {countValidPassports}");
        }

        private static List<string> ProcessInput(string file)
        {
            var text = File.ReadAllText(file);
            var lines = text.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var passports = lines.Select(l => l.Replace("\r\n", " ")).ToList();
         
            return passports;
        }

        private static bool FirstPassportPolicy(string passport)
        {
            return passport.Contains("ecl")
                && passport.Contains("pid")
                && passport.Contains("eyr")
                && passport.Contains("hcl")
                && passport.Contains("byr")
                && passport.Contains("iyr")
                && passport.Contains("hgt"); 

        }

        private static bool checkPIDPolicy(string passport)
        {
            Regex reg = new Regex(@"^(?=.*pid:\d{9}\s)", RegexOptions.Compiled);

            return reg.IsMatch(passport.Replace("\n", " ") + " ");

        }

        private static bool checkECLPolicy(string passport)
        {
            Regex reg = new Regex(@"^(?=.*ecl:(amb|blu|brn|gry|grn|hzl|oth)\s)", RegexOptions.Compiled);
            return reg.IsMatch(passport.Replace("\n", " ") + " ");
        }

        private static bool checkHCLPolicy(string passport)
        {
            Regex reg = new Regex(@"^(?=.*hcl:#[0-9a-f]{6}\s)", RegexOptions.Compiled);

            return reg.IsMatch(passport.Replace("\n", " ") + " ");
        }

        private static bool checkHGTPolicy(string passport)
        {
            Regex reg = new Regex(@"^(?=.*hgt:(((59|6[0-9]|7[0-6])in)|((1[5-8][0-9]|19[0-3])cm))\s)", RegexOptions.Compiled);
            return reg.IsMatch(passport.Replace("\n", " ") + " ");
        }

        private static bool checkEYRPolicy(string passport)
        {
            Regex reg = new Regex(@"^(?=.*eyr:((202[0-9])|2030)\s)", RegexOptions.Compiled);
            return reg.IsMatch(passport.Replace("\n", " ") + " ");
        }

        private static bool checkIYRPolicy(string passport)
        {
            Regex reg = new Regex(@"^(?=.*iyr:((201[0-9])|2020)\s)", RegexOptions.Compiled);
            return reg.IsMatch(passport.Replace("\n", " ") + " ");
        }

        private static bool checkBYRPolicy(string passport)
        {
            Regex reg = new Regex(@"^(?=.*byr:((19[2-9]\d)|200[0-2])\s)", RegexOptions.Compiled);
            return reg.IsMatch(passport.Replace("\n", " ") + " ");
        }
    }
}