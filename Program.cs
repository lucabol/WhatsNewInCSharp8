using System;
using System.Collections.Generic;

public struct Point
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Distance => Math.Sqrt(X * X + Y * Y);

    public override string ToString() =>
        $"({X}, {Y}) is {Distance} from the origin";

    public void Translate(int xOffset, int yOffset)
    {
        X += xOffset;
        Y += yOffset;
    }
}

class Program
{
    static void Main()
    {
        var p = new Point() { X = 30, Y = 40 };
        Console.WriteLine(p);
    }
}