using System.Diagnostics;

class Program
{
    static void Main()
    {
        Process.Start("pwsh", @"-File c:\dev\WhatsNewInCSharp8\Reset.ps1");
    }
}