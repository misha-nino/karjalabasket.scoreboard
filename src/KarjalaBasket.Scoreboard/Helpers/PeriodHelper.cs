using System.Globalization;

namespace KarjalaBasket.Scoreboard.Helpers;

internal static class PeriodHelper
{
    public static string PeriodToString(int currentPeriod, int totalPeriods, string format) =>
        IsExtraPeriod(currentPeriod, totalPeriods)
            ? string.Format(format, currentPeriod - totalPeriods) 
            : currentPeriod.ToString(CultureInfo.InvariantCulture);

    public static bool IsExtraPeriod(int currentPeriod, int totalPeriods) => currentPeriod > totalPeriods;
}