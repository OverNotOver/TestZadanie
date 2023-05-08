using System;

namespace TestZadanie2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 1, 2, 3, 4, 4, 56 };

            int[] newArray= new int[array.Length];

            newArray[0] = array[0];

            int lastElemnt = 0;

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] != newArray[lastElemnt])
                {
                    lastElemnt++;
                    newArray[lastElemnt] = array[i];
                }
                
            }
            int[] finalArray = new int[lastElemnt + 1];

            Array.Copy(newArray, finalArray, lastElemnt + 1);

            Console.WriteLine(string.Join(',', finalArray));
            Console.ReadKey();
        }

       //Временая сложность это длинна входного массива. 
       //Пространственная сложность, так как создаем новий масив для уникальних елементов
       
    }
}