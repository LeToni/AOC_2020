using System;
using System.Collections.Generic;

namespace Day1
{
    public class ExpenseReport
    {
        private List<Tuple<int,int>> twoTuple { get; set; }

        public ExpenseReport()
        {
            twoTuple = new List<Tuple<int, int>>();
        }

        public void Calculate(int[] input, int limit)
        {
            for(int i = 0; i < input.Length; i++)
            {
                for(int j = i + 1; j < input.Length; j++)
                {
                    if(input[i] + input[j] == limit)
                    {
                        twoTuple.Add( new Tuple<int, int>(input[i], input[j]));
                    }
                }
            }
        }

        public void DisplayStatistics()
        {
            foreach(var tuple in twoTuple)
            {
                var sum = tuple.Item1 + tuple.Item2;
                var multi = tuple.Item1 * tuple.Item2;
                Console.WriteLine($"Found Integer pair: {tuple.Item1} - {tuple.Item2} where sum {sum} and multiplication of them is {multi}");
            }
        }
    }

}