using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace uppTimer
{
    class Config
    {
        public string TimerName { get; set; }
        public TimeSpan TotalTime { get; set; }

        private const string ConfigName = @"uppTimer.cfg";

        internal void ParseConfig(string config)
        {
            TimerName = config.Split('=')[0].Trim();

            var timeConfig = config.Split('=')[1].Trim();

            var regexHours = new Regex(@"(\d+)h");
            var hours = TimeSpan.FromHours(regexHours.IsMatch(timeConfig)
                        ? double.Parse(regexHours.Match(timeConfig).Groups[1].Value)
                        : 0);

            var regexMinutes = new Regex(@"(\d+)m");
            var minutes = TimeSpan.FromMinutes(regexMinutes.IsMatch(timeConfig)
                          ? double.Parse(regexMinutes.Match(timeConfig).Groups[1].Value)
                          : 0);

            TotalTime = hours + minutes;
        }

        public void Load()
        {
            if (!File.Exists(ConfigName))
            {
                File.WriteAllText(ConfigName, @"Timer = 0m", System.Text.Encoding.UTF8);
            }

            ParseConfig(File.ReadAllText(ConfigName));
        }

        public void Save()
        {
            File.WriteAllText(ConfigName, string.Format("{0} = {1}", TimerName, GetTimeStringOld(TotalTime)), System.Text.Encoding.UTF8);
        }

        public static string GetTimeString(TimeSpan timeSpan, bool withSeconds = false)
        {
            return string.Format("{0}:{1:00}:{2:00}", Math.Floor(timeSpan.TotalHours), timeSpan.Minutes, timeSpan.Seconds);
        }
        
        private static string GetTimeStringOld(TimeSpan timeSpan, bool withSeconds = false)
        {
            var items = new List<string>();

            var hours = string.Format("{0}h", timeSpan.Hours);
            var minutes = string.Format("{0}m", timeSpan.Minutes);
            var seconds = string.Format("{0}s", timeSpan.Seconds);

            if (timeSpan.Hours != 0) items.Add(hours);

            if (withSeconds)
            {
                if (timeSpan.Minutes == 0)
                {
                    if (timeSpan.Seconds == 0 && timeSpan.Hours != 0)
                    {
                    }
                    else
                    {
                        items.Add(seconds);
                    }
                }
                else
                {
                    items.Add(minutes);
                    if (timeSpan.Seconds != 0) items.Add(seconds);
                }
            }
            else
            {
                items.Add(minutes);
            }

            return string.Join(" ", items);
        }
    }
}
