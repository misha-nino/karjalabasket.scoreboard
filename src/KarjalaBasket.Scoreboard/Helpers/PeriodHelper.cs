using System.Globalization;
using KarjalaBasket.Scoreboard.Constants;

namespace KarjalaBasket.Scoreboard.Helpers;

internal static class PeriodHelper
{
    public static string PeriodToString(int currentPeriod, string format) =>
        IsExtraPeriod(currentPeriod)
            ? string.Format(format, currentPeriod - BasketballNames.Period.Count) 
            : currentPeriod.ToString(CultureInfo.InvariantCulture);

    public static bool IsExtraPeriod(int currentPeriod) => currentPeriod > BasketballNames.Period.Count;
}