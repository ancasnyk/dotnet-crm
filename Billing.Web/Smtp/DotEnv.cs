using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace Billing.Web.Smtp
{
    /// <summary>
    /// Minimal .env reader. Values are looked up in this order:
    /// .env file, then process/machine environment variables, then Web.config appSettings.
    /// </summary>
    public static class DotEnv
    {
        private static readonly Dictionary<string, string> Values =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        static DotEnv()
        {
            foreach (var path in CandidatePaths())
            {
                if (path == null || !File.Exists(path)) continue;
                Load(path);
                break;
            }
        }

        private static IEnumerable<string> CandidatePaths()
        {
            // App root first (where .env lands on deploy), then one and two levels up
            // so a solution-level .env works when running from Visual Studio.
            string root = null;
            try
            {
                root = HostingEnvironment.IsHosted ? HostingEnvironment.MapPath("~/") : null;
            }
            catch (Exception)
            {
                root = null;
            }

            root = root ?? AppDomain.CurrentDomain.BaseDirectory;

            yield return Path.Combine(root, ".env");
            yield return Path.Combine(root, @"..\.env");
            yield return Path.Combine(root, @"..\..\.env");
        }

        private static void Load(string path)
        {
            foreach (var raw in File.ReadAllLines(path))
            {
                var line = raw.Trim();
                if (line.Length == 0 || line.StartsWith("#")) continue;

                var separator = line.IndexOf('=');
                if (separator <= 0) continue;

                var key = line.Substring(0, separator).Trim();
                var value = line.Substring(separator + 1).Trim();

                // Strip a single pair of surrounding quotes, if present.
                if (value.Length >= 2 &&
                    ((value[0] == '"' && value[value.Length - 1] == '"') ||
                     (value[0] == '\'' && value[value.Length - 1] == '\'')))
                {
                    value = value.Substring(1, value.Length - 2);
                }

                Values[key] = value;
            }
        }

        public static string Get(string key, string fallback = null)
        {
            string value;
            if (Values.TryGetValue(key, out value) && !string.IsNullOrWhiteSpace(value))
                return value;

            value = Environment.GetEnvironmentVariable(key);
            if (!string.IsNullOrWhiteSpace(value))
                return value;

            value = System.Configuration.ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrWhiteSpace(value))
                return value;

            return fallback;
        }

        public static int GetInt(string key, int fallback)
        {
            int parsed;
            return int.TryParse(Get(key), out parsed) ? parsed : fallback;
        }

        public static bool GetBool(string key, bool fallback)
        {
            bool parsed;
            return bool.TryParse(Get(key), out parsed) ? parsed : fallback;
        }
    }
}
