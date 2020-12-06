using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var collectedAnswers = ProcessInput("Input.txt");

            // Puzzle 1
            var sumOfAnswers = collectedAnswers
                                   .Select(ca => GroupQuestionAnswered(ca))
                                   .Sum();

            Console.WriteLine($"Sum of questions: {sumOfAnswers}");

            // Puzzle 2
            var sumAnsweredByAll = collectedAnswers
                                    .Select(ca => QuestionsAnsweredByAll(ca))
                                    .Sum();

            Console.WriteLine($"Sum of questions to which everyone answered in each group: {sumAnsweredByAll}");
        }

        private static List<string> ProcessInput(string path)
        {
            return File.ReadAllText(path)
                .Split("\n\n", StringSplitOptions.TrimEntries)
                .ToList();
        }

        public static int GroupQuestionAnswered(string answers)
        {
            return answers.Replace("\n", "")
                .ToCharArray()
                .Distinct()
                .Count();
        }

        public static int QuestionsAnsweredByAll(string answers)
        {
            var groupAnswer = answers.Split("\n", StringSplitOptions.None);

            var answeredByAll = groupAnswer.Aggregate<IEnumerable<char>>
                                            ((prev,next) => prev.Intersect(next).ToList())
                                    .Count();

            return answeredByAll;
        }
    
    }
}
