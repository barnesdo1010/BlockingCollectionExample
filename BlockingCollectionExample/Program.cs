using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace BlockingCollectionExample
{
    class Program
    {

        private static BlockingCollection<int> data = new BlockingCollection<int>();

        static void Main(string[] args)
        {
            var producer = Task.Factory.StartNew(() => Producer());
            var consumer = Task.Factory.StartNew(() => Consumer());
            var producer2 = Task.Factory.StartNew(() => Producer2());

            Console.Read();
        }

        private static void Producer()
        {
            //  Let's simulate an event that when triggered will add to the blocking collection.
            for (int i = 0; i < 100; ++i)
            {

                if ((i % 9) == 0)
                {
                    data.Add(i); 
                }
                Thread.Sleep(100);
            }
        }

        private static void Consumer()
        {
            // This thread will block until it sees a new item in the blocking collection.
            foreach (var item in data.GetConsumingEnumerable())
            {
                Console.WriteLine(item);
            }
        }

        private static void Producer2()
        {
            // This thread is to show that multiple threads can add to the blocking collection.
            for (int i = 0; i < 100; ++i)
            {
                if ((i % 25) == 0)
                {
                    data.Add(i * 1000);
                }
                Thread.Sleep(100);
            }
        }

    }
}
