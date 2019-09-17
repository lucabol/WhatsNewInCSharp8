using System;

interface ILogger
{
    void Log(string msg);
}

class ConsoleLogger: ILogger
{
    void ILogger.Log(string msg) => Console.WriteLine(msg);
}

class DatabaseLogger: ILogger
{
    void ILogger.Log(string msg) => Console.WriteLine($"'{msg}' inserted in DB.");
}

// ... Several other Loggers ...

enum LoggerType { Console, Database /*, ... */ }

static class LoggerFactory
{
    public static ILogger GetLogger(LoggerType ltype) => ltype switch
    {
    LoggerType.Console  => new ConsoleLogger(),
    LoggerType.Database => new DatabaseLogger(),
    _                   => throw new Exception("Logger doesn't exist..")
    };
}

public class Program
{
    static void Main()
    {
        var lc = LoggerFactory.GetLogger(LoggerType.Console);
        lc.Log("Hello");

        var ld = LoggerFactory.GetLogger(LoggerType.Database);
        ld.Log("Hello");
    }
}