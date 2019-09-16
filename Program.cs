using System;

internal class ResourceHog : IDisposable
{
    private string name;
    private bool beenDisposed;

    public ResourceHog(string name) => this.name = name;

    public void Dispose()
    {
        beenDisposed = true;
        Console.WriteLine($"Disposing {name}");
    }

    internal void CopyFrom(ResourceHog src)
    {
        switch (beenDisposed, src.beenDisposed)
        {
            case (false, false) : Console.WriteLine($"Copying from {src.name} to {name}"); return;
            case (_, _)         : throw new ObjectDisposedException($"Resource {name} has already been disposed");
        };

    }
}
public class Program
{
    static void Main()
    {
        UseResource();
    }

    internal static void UseResource()
    {
        using (var src = new ResourceHog("source"))
        {
            using (var dest = new ResourceHog("destination"))
            {
                dest.CopyFrom(src);
            }
            Console.WriteLine("After closing destination block");
        }
        Console.WriteLine("After closing source block");
    }

}