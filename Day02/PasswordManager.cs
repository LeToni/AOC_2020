using System;
using System.Collections.Generic;

namespace Day2
{
    public class PasswordManager
    {
        public List<PasswordPolicy> PasswordPolicies;

        public PasswordManager()
        {
            PasswordPolicies = new List<PasswordPolicy>();
        }

        public void AddPolicy(string min, string max, string letter, string password)
        {
            int minimum = Int32.Parse(min);
            int maximum = Int32.Parse(max);
            var newPolicy = new PasswordPolicy { FirstRule = minimum, SecondRule = maximum, Letter = Convert.ToChar(letter), Password = password };

            if(newPolicy.IsValidPolicy()){
                PasswordPolicies.Add(newPolicy);
            }
        }

        public void AddNewPolicy(string pos1, string pos2, string letter, string password)
        {
            int minimum = Int32.Parse(pos1);
            int maximum = Int32.Parse(pos2);
            var newPolicy = new PasswordPolicy { FirstRule = minimum, SecondRule = maximum, Letter = Convert.ToChar(letter), Password = password };

            if (newPolicy.IsValidNewPolicy())
            {
                PasswordPolicies.Add(newPolicy);
            }
        }
    }
}