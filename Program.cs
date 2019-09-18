using System;

class Program
{
    static void Main(string[] args)
    {
        Person luca = new Person("Luca", "Bolognese");
        var length = GetLengthOfMiddleName(luca);
        Console.WriteLine(length);
    }

    internal class Person
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public Person(string first, string last) =>
            (FirstName, LastName) = (first, last);

        public Person(string first, string middle, string last) =>
            (FirstName, MiddleName, LastName) = (first, middle, last);

        public override string ToString() => $"{FirstName} {MiddleName} {LastName}";
    }

    private static int GetLengthOfMiddleName(Person p)
    {
        string middleName = p.MiddleName;
        return middleName.Length;
    }
}
