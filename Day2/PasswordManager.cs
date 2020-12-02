using System.Collections.Generic;

namespace Day2
{
    public class PasswordManager
    {
        List<PasswordPolicy> PasswordPolicies;

        public PasswordManager()
        {
            PasswordPolicies = new List<PasswordPolicy>();
        }

        public void AddPolicy(int min, int max, char letter, string password)
        {
            var newPolicy = new PasswordPolicy { MinRule = min, MaxRule = max, Letter = letter, Password = password };

            if(newPolicy.IsValidPolicy()){
                PasswordPolicies.Add(newPolicy);
            }
        }

        public void AddPolicy(PasswordPolicy passwordPolicy) 
        {
            PasswordPolicies.Add(passwordPolicy);
        }

    }
}