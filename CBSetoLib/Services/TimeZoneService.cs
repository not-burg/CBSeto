using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CBSetoLib.Services
{
    public class TimeZoneService
    {
        private readonly Dictionary<string, TimeZoneInfo> _timeZones;

        public TimeZoneService()
        {
            var lines = File.ReadAllLines("./Timezones.txt");

            var keyValuePairs = lines.Select(line =>
                new KeyValuePair<string, TimeZoneInfo>(line, TimeZoneInfo.FindSystemTimeZoneById(line)));

            _timeZones = new Dictionary<string, TimeZoneInfo>(keyValuePairs);
        }

        public IEnumerable<KeyValuePair<string, string>> GetTimeKeyValuePairs() =>
            _timeZones.Select(timeZone =>
                new KeyValuePair<string, string>(timeZone.Key, 
                    TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, timeZone.Value).ToShortTimeString()));
    }
}
