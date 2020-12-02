namespace Day2
{
    public class PasswordPolicy
    {
        public int FirstRule { get; set; }
        public int SecondRule { get; set; }
        public char Letter { get; set; }
        public string Password { get; set; }

        public bool IsValidPolicy()
        {
            var count = 0;
            foreach (char c in Password)
            {
                if (c == Letter)
                {
                    count++;
                }
            }
            if(count >= FirstRule && count <= SecondRule)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool IsValidNewPolicy()
        {
            if(Password[FirstRule-1] == Letter ^ Password[SecondRule-1] == Letter)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}