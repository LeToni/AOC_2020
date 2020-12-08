using System;
using System.Collections.Generic;
using System.IO;

namespace Day08
{
    class Program
    {
        public const string NOP = "nop";
        public const string ACC = "acc";
        public const string JMP = "jmp";

        static Dictionary<int, bool> InstructionVisited = new Dictionary<int, bool>();
        
        static void Main(string[] args)
        {
            string file = "Input.txt";
            List<Instruction> instructions = new List<Instruction>();
            ProcessInput(file, ref instructions);

            int dryRun = DryRun(ref instructions);
            Console.WriteLine($"Value in the accumulator for first rum: {dryRun}");
        }

        public static void Run (ref List<Instruction> instructions)
        {
            Dictionary<int, string> memory = new Dictionary<int, string>();

            for(int i = 0; i < instructions.Count; i++)
            {
                if(instructions[i].Operation == ACC || instructions[i].Operation == JMP)
                {
                    memory.Add(i, instructions[i].Operation);
                }
            }
        }

        public static bool HasInfiniteLoop(ref List<Instruction> instructions, int ithOperation)
        {
            if(instructions[ithOperation].Operation == JMP)
            {
                return InstructionVisited[ithOperation];
            }
            else
            {
                return false;
            }
        }
        
        public static string InvertOperation(string operation)
        {
            switch (operation)
            {
                case ACC:
                    return JMP;
                case JMP:
                    return NOP;
                default:
                    return operation;
            }
        }

        public static int DryRun(ref List<Instruction> instructions)
        {
            int accumulator = 0;
            int i = 0;
            while (!InstructionVisited[i])
            {
                if (instructions[i].Operation == NOP)
                {
                    InstructionVisited[i] = true;
                    i++;
                }
                if(instructions[i].Operation == ACC)
                {
                    accumulator = accumulator + instructions[i].Argument;
                    InstructionVisited[i] = true;
                    i++;
                }
                if(instructions[i].Operation == JMP)
                {
                    InstructionVisited[i] = true;
                    i = i + instructions[i].Argument;
                }
            }

            return accumulator;
        }

        public static void ProcessInput(string file, ref List<Instruction> instructions)
        {
            string[] temp = File.ReadAllLines(file);
            

            for(int i = 0; i < temp.Length; i++)
            {
                var instruction = temp[i].Split(" ", StringSplitOptions.TrimEntries);
                InstructionVisited.Add(i, false);
                instructions.Add(new Instruction { Operation = instruction[0], Argument = int.Parse(instruction[1]) });
            }
        }
    }
}
