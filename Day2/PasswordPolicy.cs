namespace Day2
{
    public class PasswordPolicy
    {
        public int MinRule { get; set; }
        public int MaxRule { get; set; }
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
            if(count >= MinRule && count <= MaxRule)
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