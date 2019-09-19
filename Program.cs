using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

class Program
{
    static void Main(string[] args)
    {
        Person luca = new Person("Luca", "Bolognese");
        var length = GetLengthOfMiddleName(luca);
        Console.WriteLine(length);
    }

    class Person
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

    static int GetLengthOfMiddleName(Person p)
    {
        string middleName = p.MiddleName;
        return middleName.Length;
    }

    #region Advanced

    static void NotNullConstraint()
    {
        Exec((string)null);

        var dict = new Dictionary<string, string>();

        static void Exec<T>(T t)
        {
            Console.WriteLine(t.ToString());
        }
    }

    class Employee
    {
        private string _innerValue = string.Empty;

        public string FirstName {
            get {
                return _innerValue;
            }
            set {
                _innerValue = value ?? "";
            }
        }
    }

    static void AllowNullAttribute() {
        var c = new Employee();
        c.FirstName = null;
        //...
        // Elsewhere
        Console.WriteLine(c.FirstName.Length);
    }

    class MyHandle { public int count { get; set; } }

    static class DisallowNull
    {
        // Takes not null, but make null inside
        internal static void DisposeAndClear(ref MyHandle handle) {
            handle.count = 0;
            handle = null;
        }
    }

    #endregion
}
