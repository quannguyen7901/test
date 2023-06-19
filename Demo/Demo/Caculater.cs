namespace Misa.WebFresher032023.Demo
{
    public class Caculater
    {
        public long Add(int x, int y)
        {
            return (long)x + y;
        }
        public long Sub(int x, int y)
        {
            return (long)x - y;
        }
        public long Mul(int x, int y) { return (long)x * y; }
        public double Div(int x, int y) {
            if (y == 0)
            {
                throw new Exception("Không chia được cho 0"); 
            }
            else
            {
                return (double)x / y;
            }
        }
    }
}
