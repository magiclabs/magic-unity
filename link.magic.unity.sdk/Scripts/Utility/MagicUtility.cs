using System;

namespace link.magic.unity.sdk.Utility
{
    internal class MagicUtility
    {
        public static string Atob(string str)
        {
            byte[] optionsInBytes = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(optionsInBytes);
        }
    }
}