using Application.Commom.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commom.Helpers
{
    public static class DateTimeHelper
    {
        private static readonly DateTime DateTimeUnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static readonly string[] Month = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

        public static TimeSpan Tz = new TimeSpan(-3, 0, 0);

        public static DateTimeOffset Now => DateTimeOffset.UtcNow.ToOffset(Tz);

        public static long GetUnixTimeMilliseconds(DateTime value)
        {
            return (long)(value - DateTimeUnixEpoch).TotalMilliseconds;
        }

        public static string ReadableString(DateTimeOffset value)
        {
            return value.ToOffset(Tz).ToString("dd/MM/yyyy HH:mm");
        }

        public static bool IsLeapYear(string startPeriod)
        {
            return ((Int32.Parse(startPeriod) % 4 == 0) && (Int32.Parse(startPeriod) % 100 != 0)) || (Int32.Parse(startPeriod) % 400 == 0);
        }

        public static string ConvertMonthIntToString(int month)
        {
            return Month[month - 1];
        }

        public static (bool, string, DateTimeOffset?) Read(string value)
        {
            if (value.Length != 10)
            {
                goto WrongFormat;
            }

            var yearSb = new StringBuilder(4);
            var monthSb = new StringBuilder(2);
            var daySb = new StringBuilder(2);

            for (var i = 0; i < value.Length; i++)
            {
                var c = value[i];

                if ((i == 2 || i == 5) && c != '/')
                {
                    goto WrongFormat;
                }

                if ((i == 2 || i == 5) && c == '/')
                {
                    continue;
                }

                if (!char.IsDigit(c))
                {
                    goto WrongFormat;
                }

                if (i < 2)
                {
                    daySb.Append(c);
                }
                else if (i < 5)
                {
                    monthSb.Append(c);
                }
                else
                {
                    yearSb.Append(c);
                }
            }

            var year = int.Parse(yearSb.ToString());
            var month = int.Parse(monthSb.ToString());
            var day = int.Parse(daySb.ToString());

            return (true, null, new DateTimeOffset(year, month, day, 0, 0, 0, Tz));

        WrongFormat:
            return (false, $"Formato de valor ingresado es erróneo, se recibió '{value}' y el formato esperado es DD/MM/YYYY", null);
        }

        public static (ServiceStatus, string) ValidDates(string startDate, string endDate)
        {
            if (startDate != null)
            {
                startDate = startDate.Trim(' ', '\n', '\r');

                var (ok, message, startDateWrapper) = DateTimeHelper.Read(startDate);

                if (!ok)
                {
                    return (ServiceStatus.FailedValidation, $"Fecha de inicio: {message}");
                }
            }

            if (endDate != null)
            {
                endDate = endDate.Trim(' ', '\n', '\r');

                var (ok, message, startDateWrapper) = DateTimeHelper.Read(endDate);

                if (!ok)
                {
                    return (ServiceStatus.FailedValidation, $"Fecha de fin: {message}");
                }
            }

            return (ServiceStatus.Ok, null);
        }

        public static (string, string) DatesMonth(DateTimeOffset now)
        {
            DateTime oFirst = new DateTime(now.Year, now.Month, 1);
            DateTime oLast = oFirst.AddMonths(1).AddDays(-1);

            return (oFirst.ToString("dd/MM/yyyy"), oLast.ToString("dd/MM/yyyy"));
        }
    }
}
