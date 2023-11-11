﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Core.Common.Helpers;

public static class Extensions
{
    public static void RemoveRange<T>(this List<T> source, IEnumerable<T> rangeToRemove)
    {
        if (rangeToRemove == null | !rangeToRemove.Any())
            return;

        foreach (T item in rangeToRemove)
        {
            source.Remove(item);
        }
    }

    public static string ToTwoChar(this string num)
    {
        switch (num.Length)
        {
            case 1:
                return @"0" + num;
            default:
                return num;
        }
    }

    public static string ToThreeChar(this string num)
    {
        switch (num.Length)
        {
            case 1:
                return @"00" + num;
            case 2:
                return @"0" + num;
            default:
                return num;
        }
    }

    public static string ToFourChar(this string num)
    {
        switch (num.Length)
        {
            case 1:
                return @"000" + num;
            case 2:
                return @"00" + num;
            case 3:
                return @"0" + num;
            default:
                return num;
        }
    }

    public static string ToFiveChar(this string num)
    {
        switch (num.Length)
        {
            case 1:
                return @"0000" + num;
            case 2:
                return @"000" + num;
            case 3:
                return @"00" + num;
            case 4:
                return @"0" + num;
            default:
                return num;
        }
    }

    public static string ToSixChar(this string num)
    {
        switch (num.Length)
        {
            case 1:
                return @"00000" + num;
            case 2:
                return @"0000" + num;
            case 3:
                return @"000" + num;
            case 4:
                return @"00" + num;
            case 5:
                return @"0" + num;
            default:
                return num;
        }
    }

    public static string ToSevenChar(this string num)
    {
        switch (num.Length)
        {
            case 1:
                return @"000000" + num;
            case 2:
                return @"00000" + num;
            case 3:
                return @"0000" + num;
            case 4:
                return @"000" + num;
            case 5:
                return @"00" + num;
            case 6:
                return @"0" + num;
            default:
                return num;
        }
    }

    public static string ToEightChar(this string num)
    {
        switch (num.Length)
        {
            case 1:
                return @"0000000" + num;
            case 2:
                return @"000000" + num;
            case 3:
                return @"00000" + num;
            case 4:
                return @"0000" + num;
            case 5:
                return @"000" + num;
            case 6:
                return @"00" + num;
            case 7:
                return @"0" + num;
            default:
                return num;
        }
    }

    public static string ToNineChar(this string num)
    {
        switch (num.Length)
        {
            case 1:
                return @"00000000" + num;
            case 2:
                return @"0000000" + num;
            case 3:
                return @"000000" + num;
            case 4:
                return @"00000" + num;
            case 5:
                return @"0000" + num;
            case 6:
                return @"000" + num;
            case 7:
                return @"00" + num;
            case 8:
                return @"0" + num;
            default:
                return num;
        }
    }

    public static string ToTenChar(this string num)
    {
        switch (num.Length)
        {
            case 1:
                return @"000000000" + num;
            case 2:
                return @"00000000" + num;
            case 3:
                return @"0000000" + num;
            case 4:
                return @"000000" + num;
            case 5:
                return @"00000" + num;
            case 6:
                return @"0000" + num;
            case 7:
                return @"000" + num;
            case 8:
                return @"00" + num;
            case 9:
                return @"0" + num;
            default:
                return num;
        }
    }

    public static string ToCharLength(this string num, string character, int length)
    {
        var stringLength = num.Length;
        if (stringLength >= length)
            return num;

        var remainingLength = length - num.Length;
        var rtn = num;
        for (var i = 0; i < remainingLength; i++)
        {
            rtn += $"{character}";
        }

        return rtn;
    }

    public static string ToTenSpaces(this string num)
    {
        switch (num.Length)
        {
            case 1:
                return @"         " + num;
            case 2:
                return @"        " + num;
            case 3:
                return @"       " + num;
            case 4:
                return @"      " + num;
            case 5:
                return @"     " + num;
            case 6:
                return @"    " + num;
            case 7:
                return @"   " + num;
            case 8:
                return @"  " + num;
            case 9:
                return @" " + num;
            default:
                return num;
        }
    }

    public static string ToTitleCase(this string s)
    {
        if (s == null) return s;

        string[] words = s.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length == 0) continue;

            char firstChar = Char.ToUpper(words[i][0]);
            string rest = "";
            if (words[i].Length > 1)
            {
                rest = words[i].Substring(1).ToLower();
            }
            words[i] = firstChar + rest;
        }
        return String.Join(" ", words).Trim();
    }

    public static double ToNumValue(this object obj)
    {
        double i;
        try
        {
            Double.TryParse(obj.ToString(), out i);
            return i;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static double AddValue(this double initial, double value) => initial += value;

    public static int ToIntegerValue(this object obj)
    {
        try
        {
            int i;
            Int32.TryParse(obj.ToString(), out i);
            return i;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static int ToAgeInYears(this DateTime birthDate, DateTime currentDate)
    {
        var yearsDifference = currentDate.Year - birthDate.Year;

        // Check if the second date's month and day are less than the first date's month and day
        if (currentDate.Month < birthDate.Month || (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day))
        {
            yearsDifference--;
        }

        return yearsDifference;
    }

    public static (int, int) ToAgeInYearsAndMonths(this DateTime birthDate, DateTime currentDate)
    {
        var years = currentDate.Year - birthDate.Year;
        var months = currentDate.Month - birthDate.Month;

        if (currentDate.Day < birthDate.Day)
        {
            months--;
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        return (years, months);
    }

    public static double ToDecimalValue(this string value)
    {
        try
        {
            var separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            value = value.Replace(".", separator).Replace(",", separator);
            return Convert.ToDouble(value);
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static double ToRoundedUpValue(this double amount, int rounding)
    {
        if (amount % rounding == 0)
        {
            return amount;
        }

        var result = (int)Math.Round((amount + 0.5 * rounding) / rounding);
        if (amount > 0 && result == 0)
        {
            result = 1;
        }
        return result * rounding;
    }

    public static double ToRoundedDownValue(this double amount, int rounding)
    {
        var result = (int)Math.Round((amount - 0.5 * rounding) / rounding);
        if (amount > 0 && result == 0)
        {
            result = 1;
        }
        return result * rounding;
    }

    public static double ToRoundedValue(this double amount, int rounding)
    {
        var result = (int)Math.Round(amount / rounding);
        if (amount > 0 && result == 0)
        {
            result = 1;
        }
        return result * rounding;
    }

    public static DateTime ToDateTime(this string stringDate)
    {
        var convertedDate = Convert.ToDateTime(stringDate);
        return convertedDate;
    }

    public static DateTime ToMonthEndDate(this DateTime date)
    {
        var year = date.Year;
        var month = date.Month;
        var createDate = new DateTime(year, month, DateTime.DaysInMonth(year, month)).ToDateWithoutTime();
        return createDate;
    }

    public static DateTime ToDateWithoutTime(this DateTime date)
    {
        var createDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        return createDate;
    }

    public static DateTime ToUtcDate(this DateTime date) => new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc);

    public static string ToDatabaseDateString(this DateTime dateTime)
    {
        return
            $"CAST(N'{dateTime.Year.ToString().ToFourChar()}-{dateTime.Month.ToString().ToTwoChar()}-{dateTime.Day.ToString().ToTwoChar()} 00:00:00.000' AS DateTime)";
    }

    public static string ToMaxLength(this string num, int len)
    {
        if (string.IsNullOrEmpty(num)) return string.Empty;
        return num.Trim().Length <= len ? num.Trim() : num.Trim().Substring(0, len);
    }

    public static string ToDayMonthYearFormat(this DateTime num)
    {
        if (num.Date == DateTime.MinValue)
            return string.Empty;
        return
            $"{num.Day.ToString().ToTwoChar()}/{num.Month.ToString().ToTwoChar()}/{num.Year.ToString().ToFourChar()}";
    }

    public static string ClearSeparatorCharacter(this string dirtyString, string separator) =>
        string.IsNullOrEmpty(dirtyString) ? string.Empty : dirtyString.ToString().ToNormalizeNameString().Replace(separator, string.Empty);

    public static string ToNormalizeNameString(this string name, bool upperCase = true)
    {
        var normalizedName = string.Empty;

        if (string.IsNullOrEmpty(name)) return normalizedName;

        var source = "ÀÁÂÃÄÅÇÈÉÊËÌÍÎÏÑÒÓÔÕÖÙÚÛÜÝàáâãäåçèéêëìíîïðñòóôõöùúûüýÿ".ToCharArray();
        var target = "AAAAAACEEEEIIIINOOOOOUUUUYaaaaaaceeeeiiiionooooouuuuyy".ToCharArray();

        normalizedName = name.Replace("œ", "oe").Replace("Œ", "OE").Replace("æ", "ae").Replace("Æ", "AE").Replace("°", "o")
            .Replace("\r\n", "").Replace("\"", "").Replace("  ", " ");

        for (var i = 0; i < source.Length - 1; i++)
        {
            normalizedName = normalizedName.Replace(source[i], target[i]);
        }

        if (upperCase) normalizedName = normalizedName.ToUpper();

        return normalizedName;
    }

    public static bool IsValidDate(this string date) => DateTime.TryParse(date, out _);
}