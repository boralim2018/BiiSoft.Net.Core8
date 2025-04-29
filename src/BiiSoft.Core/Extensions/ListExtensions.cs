using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Collections.Extensions;
using BiiSoft.Enums;

namespace BiiSoft.Extensions
{
    public static class ListExtensions
    {
        public static List<T> ToList<T>(this T enumType) where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        public static List<string> ToListStr<T>(this T enumType) where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(e => e.ToString()).ToList();
        }
        public static List<string> NameList<T>(this T enumType) where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(e => e.GetName()).ToList();
        }

        public static List<string> ToIndirectList(this AccountType type)
        {
            return AccountType.Cash.ToList().Select(s => $"{s.GetName()}|{string.Join(",", s.SubTypes().Select(t => t.GetName()))}").ToList();
        }

        public static KeyValuePair<string, List<string>> ToIndirectKeyValues(this string indirectListItem)
        {
            var keyValues = indirectListItem.Split('|');
            if (keyValues.Length == 0 || keyValues[0].IsNullOrEmpty()) return new KeyValuePair<string, List<string>>();
            var key = keyValues[0];
            var values = keyValues.Length > 1 ? keyValues[1].Split(',').ToList() : new List<string>();
            return new KeyValuePair<string, List<string>>(key, values);
        }

        public static string ToIndirectListItem(this KeyValuePair<string, List<string>> indirectKeyValues)
        {
            if (indirectKeyValues.Key.IsNullOrEmpty()) return string.Empty;
            var key = indirectKeyValues.Key;
            var values = indirectKeyValues.Value.Count > 0 ? string.Join(",", indirectKeyValues.Value) : string.Empty;
            return $"{key}|{values}";
        }
    }
}
