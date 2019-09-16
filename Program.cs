using System;
using System.Linq;
using ConsumerVehicleRegistration;
using CommercialRegistration;
using LiveryRegistration;

namespace ConsumerVehicleRegistration
{
    public class Car
    {
        public int Passengers { get; set; }
    }
}

namespace CommercialRegistration
{
    public class DeliveryTruck
    {
        public int GrossWeightClass { get; set; }
    }
}

namespace LiveryRegistration
{
    public class Taxi
    {
        public int Fares { get; set; }
    }

    public class Bus
    {
        public int Capacity { get; set; }
        public int Riders { get; set; }
    }
}

class Program
{
    public static void Main()
    {
        var soloDriver     = new Car();
        var twoRideShare   = new Car { Passengers = 1 };
        var threeRideShare = new Car { Passengers = 2 };
        var fullVan        = new Car { Passengers = 5 };
        var emptyTaxi      = new Taxi();
        var singleFare     = new Taxi { Fares = 1 };
        var doubleFare     = new Taxi { Fares = 2 };
        var fullVanPool    = new Taxi { Fares = 5 };
        var lowOccupantBus = new Bus { Capacity = 90, Riders = 15 };
        var normalBus      = new Bus { Capacity = 90, Riders = 75 };
        var fullBus        = new Bus { Capacity = 90, Riders = 85 };

        var heavyTruck     = new DeliveryTruck { GrossWeightClass = 7500 };
        var truck          = new DeliveryTruck { GrossWeightClass = 4000 };
        var lightTruck     = new DeliveryTruck { GrossWeightClass = 2500 };

        Console.WriteLine($"The toll for a solo driver is {CalculateToll(soloDriver)}");
        Console.WriteLine($"The toll for a two ride share is {CalculateToll(twoRideShare)}");
        Console.WriteLine($"The toll for a three ride share is {CalculateToll(threeRideShare)}");
        Console.WriteLine($"The toll for a fullVan is {CalculateToll(fullVan)}");

        Console.WriteLine($"The toll for an empty taxi is {CalculateToll(emptyTaxi)}");
        Console.WriteLine($"The toll for a single fare taxi is {CalculateToll(singleFare)}");
        Console.WriteLine($"The toll for a double fare taxi is {CalculateToll(doubleFare)}");
        Console.WriteLine($"The toll for a full van taxi is {CalculateToll(fullVanPool)}");

        Console.WriteLine($"The toll for a low-occupant bus is {CalculateToll(lowOccupantBus)}");
        Console.WriteLine($"The toll for a regular bus is {CalculateToll(normalBus)}");
        Console.WriteLine($"The toll for a bus is {CalculateToll(fullBus)}");

        Console.WriteLine($"The toll for a truck is {CalculateToll(heavyTruck)}");
        Console.WriteLine($"The toll for a truck is {CalculateToll(truck)}");
        Console.WriteLine($"The toll for a truck is {CalculateToll(lightTruck)}");

        try
        {
            CalculateToll("this will fail");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Caught an argument exception when using the wrong type");
        }
        try
        {
            CalculateToll(null!);
        }
        catch (ArgumentNullException)
        {
            Console.WriteLine("Caught an argument exception when using null");
        }
    }

    public static decimal CalculateToll(object vehicle) =>

        vehicle switch
        {
            Car _ => 2.00m,
            Taxi _ => 3.50m,
            Bus _ => 5.00m,
            DeliveryTruck _ => 10.00m,
            { } => throw new ArgumentException(message: "Not a known vehicle type", paramName: nameof(vehicle)),
            null => throw new ArgumentNullException(nameof(vehicle))
        };

    // Cars and taxis with no passengers pay an extra $0.50.
    // Cars and taxis with two passengers get a 0.50 discount.
    // Cars and taxis with three or more passengers get a $1.00 discount.
    // Buses that are less than 50% full pay an extra $2.00.
    // Buses that are more than 90% full get a $1.00 discount.
    // Use nested switches

    #region Advanced
    private static bool IsWeekDay(DateTime timeOfToll) =>
        timeOfToll.DayOfWeek switch // simplify
        {
            DayOfWeek.Monday    => true,
            DayOfWeek.Tuesday   => true,
            DayOfWeek.Wednesday => true,
            DayOfWeek.Thursday  => true,
            DayOfWeek.Friday    => true,
            DayOfWeek.Saturday  => false,
            DayOfWeek.Sunday    => false,
            _                   => throw new Exception("Never here")
        };

    private enum TimeBand
    {
        MorningRush,
        Daytime,
        EveningRush,
        Overnight
    }

    private static TimeBand GetTimeBand(DateTime timeOfToll)
    {
        int hour = timeOfToll.Hour;
        if (hour < 6)       return TimeBand.Overnight;
        else if (hour < 10) return TimeBand.MorningRush;
        else if (hour < 16) return TimeBand.Daytime;
        else if (hour < 20) return TimeBand.EveningRush;
        else                return TimeBand.Overnight;
    }

    public decimal PeakTimePremium(DateTime timeOfToll, bool inbound) =>
                (IsWeekDay(timeOfToll), GetTimeBand(timeOfToll), inbound) switch
                {
                    (true, TimeBand.Overnight, _)       => 0.75m,
                    (true, TimeBand.Daytime, _)         => 1.5m,
                    (true, TimeBand.MorningRush, true)  => 2.0m,
                    (true, TimeBand.EveningRush, false) => 2.0m,
                    (_, _, _)                           => 1.0m,
                };
    #endregion

}
