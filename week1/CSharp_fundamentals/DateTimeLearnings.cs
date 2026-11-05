public class DateTimeLearnings
{
    public static void Run()
    {
        DateTime now = DateTime.Now;
        Console.WriteLine(now);
        Console.WriteLine(DateTime.Today);
        Console.WriteLine(DateTime.UtcNow);

        DateTime joiningDate = new DateTime(2026, 11, 2);
        // adding time
        DateTime meeting = new DateTime(2026, 11, 4, 14, 30, 0);
        Console.WriteLine(now.ToString("dd-MM-yyyy"));
        Console.WriteLine(now.ToString("dd MM yyyy"));
        Console.WriteLine(now.ToString("hh:MM tt"));
        Console.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss"));
        // hh is 12 hr clock and HH is 24 hr clock

        DateTime localTime = DateTime.Now;
        DateTime utcTime = DateTime.UtcNow;

        Console.WriteLine(localTime.Kind); // Local
        Console.WriteLine(utcTime.Kind);   // Utc
        // A DateTime with Kind = Unspecified doesn't tell you which time zone it belongs to.

        // // DateTimeOffset stores a date and time together with its UTC offset.
        DateTimeOffset now1 = DateTimeOffset.Now;
        Console.WriteLine(now1);

        // converting UTC to local DateTime
        DateTime local = utcTime.ToLocalTime();
        Console.WriteLine(local);

        // using DateTimeOffset
        DateTimeOffset utc = DateTimeOffset.UtcNow;
        DateTimeOffset indianTime = utc.ToOffset(TimeSpan.FromHours(5.5));

        Console.WriteLine(indianTime);

        // For named time zones, use TimeZoneInfo:
        TimeZoneInfo indiaZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");

        DateTime indianTime2 = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indiaZone);

        Console.WriteLine(indianTime2);

        // TimeSpan represents a duration or difference between two dates/times.
        TimeSpan duration = new TimeSpan(2,30,0);
        // the arugments here are h,m,s 
        Console.WriteLine(duration);

        TimeSpan durationHr = TimeSpan.FromHours(2.5);
        TimeSpan minutes = TimeSpan.FromMinutes(45);
        TimeSpan seconds = TimeSpan.FromSeconds(30);

        DateTime start = new DateTime(2026,11,4,9,0,0);
        DateTime end = new DateTime(2026,11,4,17,30,0);

        TimeSpan workingHr = end - start;

        Console.WriteLine(workingHr);
        Console.WriteLine(workingHr.TotalHours);

        DateTime nowT = DateTime.Now;

        DateTime tomorrow = now.AddDays(1);
        DateTime nextWeek = now.AddDays(7);
        DateTime afterTwoHr = now.AddHours(2);
        DateTime yesterday = now.AddDays(-1);

    }
}