using System;
using System.Collections.Generic;

namespace Day1
{
    public class ExtendedExpenseReport
    {
        private List<Tuple<int,int, int>> threeTuple { get; set; }

        public ExtendedExpenseReport()
        {
            threeTuple = new List<Tuple<int, int, int>>();
        }

        public void Calculate(int[] input, int limit)
        {
            bool foundTriple = false;

            for(int i = 0; i < input.Length; i++)
            {
                for(int j = i + 1; j < input.Length; j++)
                {
                    if(input[i] + input[j] >= limit)
                    {
                        break;
                    }
                    
                    for(int k = j + 1; k < input.Length; k++)
                    {
                        if(input[i] + input[j] + input[k] == limit)
                        {
                            threeTuple.Add( new Tuple<int, int, int>(input[i], input[j], input[k]));
                            foundTriple = true;
                            break;
                        }
                        if(input[i] + input[j] + input[k] > limit)
                        {
                            break;
                        }
                    }
                    if(foundTriple){
                        foundTriple = false;
                        break;
                    }
                }
            }
        }

        public void DisplayStatistics()
        {
            foreach(var tuple in threeTuple)
            {
                var sum = tuple.Item1 + tuple.Item2 + tuple.Item3;
                var multi = tuple.Item1 * tuple.Item2 * tuple.Item3;
                Console.WriteLine($"Found Integer tuple: {tuple.Item1} - {tuple.Item2} - {tuple.Item3} where sum {sum} and multiplication of them is {multi}");
            }
        }
    }
}