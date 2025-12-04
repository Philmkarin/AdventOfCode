namespace _2023
{
    public class CardComparer : IComparer<char>
    {
        public int Compare(char stringA, char stringB)
        {
            return ToInt(stringA).CompareTo(ToInt(stringB));

            int ToInt(char c)
            {
                switch (c)
                {
                    case 'A': return 14;
                    case 'K': return 13;
                    case 'Q': return 12;
                    case 'J': return 11;
                    case 'T': return 11;
                    default:
                        return c - '0';
                }
            }
        }
    }
}
