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

    /* NotNull constraint */
    static void NotNullConstraint()
    {
        Exec((string)null);

        var dict = new Dictionary<string, string>();

        static void Exec<T>(T t)
        {
            Console.WriteLine(t.ToString());
        }
    }

    /* AllowNull attribute */
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

    /* DisallowNull attribute */
    class MyHandle { public int count { get; set; } }

    static class DisallowNull
    {
        // Takes not null, but make null inside
        internal static void DisposeAndClear(ref MyHandle handle) {
            handle.count = 0;
            handle = null;
        }

        internal static void DisallowNullSample()
        {
            MyHandle handle = null;
            DisallowNull.DisposeAndClear(ref handle);
        }
    }
    /* return:MaybeNull .. NotNull attributes*/
    public static class MyArray
    {
        // Result is the default of T if no match is found
        public static T Find<T>(T[] array, Func<T, bool> match)
        {
            return default(T);
        }

#nullable enable
        // Never gives back a null when called
        public static void Resize<T>(ref T[]? array, int newSize)
        {
            if (array == null)
                array = new T[] { };
            else
                array.CopyTo(array, newSize);
        }

        internal static void MaybeNullNotNull()
        {
            string[] testArray = { "bob" };
            var value = MyArray.Find<string>(testArray, s => s == "Hello!");
            Console.WriteLine(value.Length); // Warning: Dereference of a possibly null reference.

            string[]? nullArray = null;
#nullable restore
            MyArray.Resize<string>(ref nullArray, 200);
            Console.WriteLine(nullArray.Length); // Safe!
        }
    }

    /* MaybeNullWhen(bool) and NotNullWhen(bool) */

#nullable enable
    public class MyString
    {
        // True when 'value' is null
        public static bool IsNullOrEmpty(string? value)
        {
            if (value == null) return true; else return false;
        }
    }

    public class MyVersion
    {
        // If it parses successfully, the Version will not be null.
        public static bool TryParse(string? input, out Version? version)
        {
            version = new Version();
            return true;
        }
    }

    public class MyQueue<T>
    {
        // 'result' could be null if we couldn't Dequeue it.
        public bool TryDequeue(out T result)
        {
            result = default(T)!;
            return true;
        }
    }

    static void MaybeNutNullConditional(string? s)
    {
        if (MyString.IsNullOrEmpty(s))
        {
            // This would generate a warning:
            //Console.WriteLine(s.Length); // Safe
            return;
        }

        //Console.WriteLine(s.Length); // Safe!

        if (!MyVersion.TryParse(s, out var version))
        {
            // This would generate a warning:
            // Console.WriteLine(version.Major);
            return;
        }

        //Console.WriteLine(version.Major); // Safe!

        var q = new MyQueue<string>();

        if (!q.TryDequeue(out var s1))
        {
            // This would generate a warning:
            // Console.WriteLine(s1.Length);
            return;
        }

        //Console.WriteLine(s.Length); // Safe!
    }
#nullable restore

    /* NotNullIfNotNull(string) attribute */

#nullable enable
    class MyPath
    {
        public static string? GetFileName(string? path) => path == null ? null : "bob";
    }

    static void PathTest(string? path)
    {
        var possiblyNullPath = MyPath.GetFileName(path);
        // Console.WriteLine(possiblyNullPath.Length); // Unsafe

        if (!string.IsNullOrEmpty(path))
        {
            var goodPath = MyPath.GetFileName(path);
            // Console.WriteLine(goodPath.Length); // Safe!
        }
    }
#nullable restore
    #endregion
}
