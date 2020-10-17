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

        public IEnumerable<string> GetTimeStrings() =>
            _timeZones.Select(timeZone =>
                timeZone.Key + '\t' + TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, timeZone.Value)
                    .ToLongTimeString());
    }
}
