namespace Handmade_HTTP_Server.Server.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class CoreValidator
    {
        public static void ThrowIfNullOrEmpty(string value, string name)
        {
            if (string.IsNullOrEmpty(value)){
                throw new ArgumentNullException($"{name} cannot be null or empty.", name);
            }
        }
    }
}

