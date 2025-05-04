using System;
using System.Text.RegularExpressions;
using Abp.Extensions;

namespace BiiSoft.Extensions
{
    public static class StringExtensions
    {

        public static string RemoveExtension(this string str)
        {
            return str.LastIndexOf('.') > 0 ? str.Substring(0, str.LastIndexOf('.')) : str;
        }

        public static string NoSpaces(this string str)
        {
            return str.Replace(" ", "");
        }

    }
}
