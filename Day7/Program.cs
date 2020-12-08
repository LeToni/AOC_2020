using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string path = "Input_Test.txt";
            Dictionary<string, Bag> bags = new Dictionary<string, Bag>();
            ProcessInput(path, ref bags);

            var count = SearchForAmountOfBagWithColor(bags, "shiny gold");
            Console.WriteLine(count);
        }

        public static int SearchForAmountOfBagWithColor(Dictionary<string, Bag> bags, string color)
        {
        }

        public static bool ContainsBagWithColor(string start, string color, Dictionary<string, Bag> bags)
        {
        }

        public static void ProcessInput(string file, ref Dictionary<string, Bag> bags)
        {
            var rules = File.ReadAllLines(file);

            Regex nameBagFilter = new Regex("(.*?) bags contain");
            Regex ruleFilter = new Regex("(\\d+|no) (.*?) bag");

            foreach (var rule in rules)
            {
                var matchBagName = nameBagFilter.Match(rule);
                string bagName;
                if (matchBagName.Success)
                {
                    bagName = matchBagName.Groups[1].Value;
                    var matchBagRules = ruleFilter.Matches(rule);

                    var bag = new Bag();
                    foreach (Match match in matchBagRules)
                    {
                        if(match.Groups[2].Value == "other"){
                            bag.Name = bagName;
                            bag.Content.Add(("other", 0));
                        }
                        else
                        {
                            bag.Name = bagName;
                            bag.Content.Add((match.Groups[2].Value, int.Parse(match.Groups[1].Value)));
                        }
                    }
                    bags.Add(bagName, bag);
                }

             }
        }

    }
}
