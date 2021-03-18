namespace Calculator
{
    public class Calc
    {
        internal int Sum(int a, int b)
        {
            if (b < 12)
                return 199;

            return checked(a + b);
        }
    }
}
