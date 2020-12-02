using System;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = "./Passwords.txt";
            ProcessFile(filePath);
        }

        private static void ProcessFile(string path)
        {
            string[] processecontent;
            var file = File.ReadAllLines(path);

            foreach(var content in file) 
            {
                processecontent = content.Split(' ');
            }        
        }
    }
}
