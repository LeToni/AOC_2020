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
            string path = "Input.txt";
            Dictionary<string, Bag> bags = new Dictionary<string, Bag>();
            ProcessInput(path, ref bags);

            var count = SearchForAmountOfBagWithColor(bags, "shiny gold");
            Console.WriteLine($"Bags that can eventually contain one shiny gold: {count}");

            var amount = CountBagContainsAmountOfBag("shiny gold", bags);
            Console.WriteLine($"Amount of individual bags are required: {amount}");
        }

        public static int CountBagContainsAmountOfBag(string color, Dictionary<string,Bag> bags)
        {
            return CountAmountOfBagsContained(color, bags) - 1;
        }

        public static int CountAmountOfBagsContained(string currentBag, Dictionary<string, Bag> bags)
        {
            int count = 1;
            if (!bags.ContainsKey(currentBag))
            {
                return count;
            }

            var bag = bags[currentBag];

            foreach(var b in bag.Content)
            {
                count = count + b.Amount * CountAmountOfBagsContained(b.ColorBag, bags);
            }

            return count;
        }

        public static int SearchForAmountOfBagWithColor(Dictionary<string, Bag> bags, string color)
        {
            int count = 0;
            foreach(var bag in bags)
            {
                if (ContainsBagWithColor(bag.Key, color, bags))
                {
                    count++;
                }
            }

            return count;
        }

        public static bool ContainsBagWithColor(string start, string color, Dictionary<string, Bag> bags)
        {
            if(!bags.ContainsKey(start))
            {
                return false;
            }

            var bag = bags[start];

            foreach(var subBag in bag.Content)
            {
                if (subBag.ColorBag == color)
                {
                    return true;
                }

                if (ContainsBagWithColor(subBag.ColorBag, color, bags))
                {
                    return true;
                }
            }

            return false;
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
