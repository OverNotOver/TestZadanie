using System.Collections.Immutable;
using System.Runtime.ConstrainedExecution;

namespace TestZadanie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sentence = "The “C# Professional” course includes the topics I discuss in my CLR via C# book and teaches how the CLR works thereby showing you how to develop applications and reusable components for the .NET Framework.";


            string[] words = sentence.Split(' ');

            Dictionary<int, List<string>> wordGroups = new Dictionary<int, List<string>>();

            foreach (string word in words)
            {
                int length = word.Length;
                if (!wordGroups.ContainsKey(length))
                {
                    wordGroups[length] = new List<string>();
                }
                if (!wordGroups[length].Contains(word))
                {
                    wordGroups[length].Add(word);
                }
            }

            var sortWordGroups = wordGroups.OrderBy(x => x.Key);
            foreach (var group in sortWordGroups)
            {
                Console.WriteLine($"Words of length {group.Key}, Count: {group.Value.Count}");

                foreach (string word in group.Value)
                {
                    Console.WriteLine(word);
                }
            }
            Console.ReadKey();
        }

    }
}


