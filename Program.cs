using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable

class Program
{
    public static async Task Main()
    {
        //NullableDemo.NullableRefTypes();
        await AsyncStreamDemo.AsyncStream();
    }

    class NullableDemo
    {
        internal class Person
        {
            public string FirstName { get; set; }
            public string? MiddleName { get; set; }
            public string LastName { get; set; }

            public Person(string first, string last) =>
                (FirstName, LastName) = (first, last);

            public Person(string first, string middle, string last) =>
                (FirstName, MiddleName, LastName) = (first, middle, last);

            public override string ToString() => $"{FirstName} {MiddleName} {LastName}";
        }

        private static int GetLengthOfMiddleName(Person p)
        {
            string? middleName = p.MiddleName;
            return middleName?.Length ?? 0;
        }


        public static void NullableRefTypes()
        {
            Person miguel = new Person("Luca", "Bolognese");
            var length = GetLengthOfMiddleName(miguel);
            Console.WriteLine(length);
        }
    }

    class AsyncStreamDemo
    {
        internal static async IAsyncEnumerable<int> GenerateSequence()
        {
            for (int i = 0; i < 20; i++)
            {
                // every 3 elements, wait 2 seconds:
                if (i % 3 == 0)
                    await Task.Delay(2000);
                yield return i;
            }
        }

        public async static Task AsyncStream()
        {
            await foreach (var number in GenerateSequence())
            {
                Console.WriteLine($"The time is {DateTime.Now:hh:mm:ss}. Retrieved {number}");
            }
        }
    }
}
