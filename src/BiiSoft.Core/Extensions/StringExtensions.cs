using Abp.Domain.Entities;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BiiSoft.Extensions
{
    public static class StringExtensions
    {

        public static string RemoveExtension(this string str)
        {
            return str.LastIndexOf('.') > 0 ? str.Substring(0, str.LastIndexOf('.')) : str;
        }

        /// <summary>
        /// Validate if code is correct format. It must start with prefix and same length as provided. 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="length"></param>
        /// <param name="prefix"></param>
        /// <returns>Ex: Length = 4 and Prefix = Z => Z001, Z002, Z999 are true but Z00, Z0001, X001 are false</returns>
        public static bool IsCode(this string code, int length, string prefix = "")
        {
            //https://regex101.com/r/cG4kD9/1
            var expression = @"^P[0-9]{M,M}$".Replace("P", prefix).Replace("M", $"{length - prefix.Length}");
            return !code.IsNullOrWhiteSpace() && new Regex(expression).IsMatch(code);
        }

        /// <summary>
        /// Generate next code same as code format. 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="prefix"></param>
        /// <returns>Ex: Code = A001 return A002, Code = X099 return X100, Code = Z999 return Z1000 </returns>
        public static string NextCode(this string code, string prefix = "")
        {
            if (code.IsNullOrWhiteSpace() || !code.IsCode(code.Length, prefix)) return code;

            var next = Convert.ToInt64(code.Remove(0, prefix.Length)) + 1;
            
            return next.GenerateCode(code.Length, prefix);
        }

        /// <summary>
        /// Compare between two codes and return the bigger one. 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="compareCode"></param>
        /// <param name="prefix"></param>
        /// <returns> Ex: A001 and A002 => Return A002</returns>
        public static string MaxCode(this string code, string compareCode, string prefix = "")
        {
            var newCode = Convert.ToInt64(compareCode.Remove(0, prefix.Length));
            var oldCode = Convert.ToInt64(code.Remove(0, prefix.Length));
            if (newCode > oldCode) return compareCode;
            return code;
        }

        public static bool IsNullOrEmpty(this Guid? id)
        {
            return !id.HasValue || id.Value == Guid.Empty;
        }

        public static bool IsNullOrZero(this long? id)
        {
            return !id.HasValue || id.Value == 0;
        }

    }
}
