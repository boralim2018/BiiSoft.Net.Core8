using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Helper
{
    public enum LicenseRegisterResult { NotRegister, Registered, RegisterSuccess, RegisterFailed, InvalidProductKey, InvalidLicenseKey }
    public enum KeyRegister { ProductKey, LicenseKey, RegisteredDate }

    public class LicenseRegister
    {
        private static string GetMD5(string key)
        {
            // byte array representation of that string
            byte[] encodedPassword = new UTF8Encoding().GetBytes(key);

            // need MD5 to calculate the hash
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            // string representation (similar to UNIX format)
            string encoded = BitConverter.ToString(hash)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               //.ToLower()
               ;

            return encoded;
        }
        private static string FormatKey(string key, int length = 32, int digit = 5)
        {
            var licenseKey = "";
            for (var i = 0; i < length; i += digit)
            {
                if (licenseKey != "") licenseKey += "-";

                var remainString = key.Substring(i);

                if (remainString.Length >= digit)
                {
                    licenseKey += key.Substring(i, digit);
                }
                else
                {
                    licenseKey += remainString;
                }

                if (licenseKey.Length >= length + i / digit)
                {
                    return licenseKey.Substring(0, length + i / digit);
                }

            }

            return licenseKey;
        }
        public static string GetLicenseKey(string key)
        {
            var hasKey = GetMD5(KeyRegister.LicenseKey.ToString("") + key);

            return FormatKey(hasKey, 32);
        }
        //public static string GetProductKey(string key = "")
        //{
        //    if (key == "")
        //    {
        //        key = Helper.Functions.GetMainboardId();
        //    }

        //    var hasKey = GetMD5(KeyRegister.ProductKey.ToString("") + key);

        //    return FormatKey(hasKey, 30);
        //}

        //public static LicenseRegisterResult CheckLicenseRegister(string productKey = "")
        //{
        //    try
        //    {
        //        if (productKey == "")
        //        {
        //            var key = Helper.Functions.GetMainboardId();
        //            productKey = LicenseRegister.GetProductKey(key);
        //        }

        //        var registerKey = Registry.CurrentUser.OpenSubKey(productKey, true);
        //        if (registerKey == null) return LicenseRegisterResult.NotRegister;

        //        var registeredProductKey = registerKey.GetValue(KeyRegister.ProductKey.ToString(""));
        //        if (!productKey.Equals(registeredProductKey)) return LicenseRegisterResult.InvalidProductKey;


        //        var registeredLicenseKey = registerKey.GetValue(KeyRegister.LicenseKey.ToString(""));
        //        var licenseGenerate = GetLicenseKey(productKey);
        //        return registeredLicenseKey.Equals(licenseGenerate) ? LicenseRegisterResult.Registered : LicenseRegisterResult.InvalidLicenseKey;
        //    }
        //    catch (Exception)
        //    {
        //        return LicenseRegisterResult.NotRegister;
        //    }
        //}

        //public static LicenseRegisterResult RegisterLicense(string productKey, string licenseKey)
        //{
        //    var checkProduct = LicenseRegister.GetProductKey();
        //    if (!checkProduct.Equals(productKey))
        //    {
        //        return LicenseRegisterResult.InvalidProductKey;
        //    }

        //    var licenseGenerate = GetLicenseKey(productKey);
        //    if (!licenseGenerate.Equals(licenseKey))
        //    {
        //        return LicenseRegisterResult.InvalidLicenseKey;
        //    }

        //    RegistryKey registerKey;

        //    try
        //    {
        //        registerKey = Registry.CurrentUser.CreateSubKey(productKey);
        //    }
        //    catch (Exception)
        //    {
        //        registerKey = Registry.CurrentUser.OpenSubKey(productKey, true);
        //    }

        //    if (registerKey == null)
        //    {
        //        return LicenseRegisterResult.RegisterFailed;
        //    }

        //    registerKey.SetValue(KeyRegister.ProductKey.ToString(""), productKey);
        //    registerKey.SetValue(KeyRegister.LicenseKey.ToString(""), licenseKey);
        //    registerKey.SetValue(KeyRegister.RegisteredDate.ToString(""), DateTime.Now);

        //    return LicenseRegisterResult.RegisterSuccess;
        //}

    }
}