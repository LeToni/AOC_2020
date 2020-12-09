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

        public static Dictionary<int, bool> InstructionExecuted = new Dictionary<int, bool>();
        public static int LastInstruction = 0;

        static void Main(string[] args)
        {
            string file = "Input.txt";
            List<Instruction> instructions = new List<Instruction>();
            ProcessInput(file, ref instructions);

            var result1 = RunProgram(ref instructions);
            Console.WriteLine($"Accumulator within one run: {result1}");



            var result2 = RunAdvancedProgram(ref instructions);
            Console.WriteLine($"Accumulator with fix instruction: {result2}");
        }

        public static int RunAdvancedProgram(ref List<Instruction> instructions)
        {
            var accumulator = 0;

            foreach (var instruction in instructions)
            {
                if(instruction.Operation == ACC)
                {
                    continue;
                }

                instruction.Operation = InvertOperation(instruction.Operation);
                accumulator = RunProgram(ref instructions);

                if(LastInstruction >= instructions.Count)
                {
                    break;
                }

                instruction.Operation = InvertOperation(instruction.Operation);
            }

            return accumulator;
        }

        public static int RunProgram(ref List<Instruction> instructions)
        {
            int i = 0;
            int accumulator = 0;
            while(i < instructions.Count)
            {
                if (InstructionExecuted[i] == true)
                {
                    break;
                }

                InstructionExecuted[i] = true;

                switch (instructions[i].Operation)
                {
                    case NOP:
                        i = i + 1;
                        break;
                    case ACC:
                        accumulator = accumulator + instructions[i].Argument;
                        i = i + 1;
                        break;
                    case JMP:
                        i = i + instructions[i].Argument;
                        break;
                }

                LastInstruction = i;
            }

            
            ResetInstructionExecutedMemory();
            return accumulator;
        }

        public static void ResetInstructionExecutedMemory()
        {
            foreach(var instruction in InstructionExecuted.Keys)
            {
                InstructionExecuted[instruction] = false;
            }
        }

        public static string InvertOperation(string operation)
        {
            return operation switch
            {
                NOP => JMP,
                JMP => NOP,
                _ => operation,
            };
        }

        public static void ProcessInput(string file, ref List<Instruction> instructions)
        {
            string[] temp = File.ReadAllLines(file);
            

            for(int i = 0; i < temp.Length; i++)
            {
                var instruction = temp[i].Split(" ", StringSplitOptions.TrimEntries);
                InstructionExecuted.Add(i, false);
                instructions.Add(new Instruction { Operation = instruction[0], Argument = int.Parse(instruction[1]) });
            }
        }
    }
}
