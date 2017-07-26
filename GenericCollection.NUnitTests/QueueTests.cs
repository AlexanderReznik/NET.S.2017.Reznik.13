using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericCollections;
using NUnit.Framework;

namespace GenericCollection.NUnitTests
{
    public class QueueTests
    {
        [TestCase(new int[] {12, 15, -9, 46, 45, 23, 79, 46}, ExpectedResult = "4645237946")]
        public static string QueueIntTest(int[] arr)
        {
            Queue<int> q = new Queue<int>();
            for(int i = 0; i < 5; i++)
                q.Enqueue(arr[i]);
            for (int i = 0; i < 3; i++)
                q.Dequeue();
            for(int i = 5; i < arr.Length; i++)
                q.Enqueue(arr[i]);

            StringBuilder sb = new StringBuilder();
            foreach (var i in q)
            {
                sb.Append(i);
            }
            return sb.ToString();
        }

        [TestCase( "12", "15", "-9", "46", "45", "23", "79", "46", ExpectedResult = "4645237946")]
        public static string QueueIntTest(params string[] arr)
        {
            Queue<string> q = new Queue<string>();
            for (int i = 0; i < 5; i++)
                q.Enqueue(arr[i]);
            for (int i = 0; i < 3; i++)
                q.Dequeue();
            for (int i = 5; i < arr.Length; i++)
                q.Enqueue(arr[i]);

            StringBuilder sb = new StringBuilder();
            foreach (var i in q)
            {
                sb.Append(i);
            }
            return sb.ToString();
        }
    }
}
