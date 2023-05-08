
    namespace TastZadanie3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(FiboRec(3));
            Console.WriteLine(FiboIter(3));
            Console.ReadKey();
        }

        public static int FiboRec(int n)
        {
            if(n < 0) 
            {
                throw new Exception("Не валидное число");
            }
            if (n == 0 || n == 1) return n;
            return FiboRec(n - 1) + FiboRec(n - 2);
        }

        public static int FiboIter(int n)
        {
            int res = 0;
            int a = 0;
            int b = 1;
            if (n < 0)
            {
                throw new Exception("Не валидное число");
            }
            if (n == 0 || n == 1) return n;
            for (int i = 2; i <= n; i++)
            {
                res = a + b;
                a = b;
                b = res;
            }
            return res;
        }


        //Сложность заключается в том случае, если после рекурсии идет код и мы заменяем его итеративный способом код будет непонятным, но более производительным. Но если после рекурсии нет кода, ее легко заменить циклами и это более производительно.
        //Так же рекурсия Фибоначи может быть не оффективна для больших чисел, из-за больших вичислений. 
    }
}