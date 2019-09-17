using System;

public class Program
{
    static void Main()
    {
        var fileName = "IDontExist.txt";
        var filePath = $@"c:\Really\Don't Look Here\{fileName}";
        Console.WriteLine(filePath);
    }
}