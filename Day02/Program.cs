using System;
using System.IO;

namespace Day2
{
    class Program
    {
        public static PasswordManager PasswordManager = new PasswordManager();

        static void Main(string[] args)
        {
            var filePath = "./Passwords1.txt";
            ProcessFile(filePath);

            Console.WriteLine($"Number of valid passwords according to policy: {PasswordManager.PasswordPolicies.Count}");

            foreach(var validPassword in PasswordManager.PasswordPolicies)
            {
                Console.WriteLine($"Password is according to its polcy: {validPassword.Password}");
            }
        }

        private static void ProcessFile(string path)
        {
            char[] delimeters = new char[] { '-', ';', ' ' };

            var file = File.ReadAllLines(path);

            foreach(var line in file) 
            {
                var content = line.Split(delimeters);
                content[2] = content[2].Trim(':');

                PasswordManager.AddNewPolicy(content[0],content[1],content[2],content[3]);
             }        
        }
    }
}
