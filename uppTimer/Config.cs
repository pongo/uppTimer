namespace uppTimer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    public class Config
    {
        private readonly string configName;

        public Config()
        {
            this.configName =
                Path.GetFileNameWithoutExtension(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                + ".cfg";
        }

        public string TimerName { get; set; }
        public TimeSpan TotalTime { get; set; }
        
        public static string GetTimeString(TimeSpan timeSpan, bool withSeconds = false)
        {
            return string.Format(
                "{0}:{1:00}:{2:00}", Math.Floor(timeSpan.TotalHours), timeSpan.Minutes, timeSpan.Seconds);
        }

        public void Load()
        {
            if (!File.Exists(this.configName))
            {
                File.WriteAllText(this.configName, @"Timer = 0m", Encoding.UTF8);
            }

            this.ParseConfig(File.ReadAllText(this.configName));
        }

        public void Save()
        {
            File.WriteAllText(
                this.configName, string.Format("{0} = {1}", this.TimerName, GetTimeStringOld(this.TotalTime)), Encoding.UTF8);
        }

        internal void ParseConfig(string config)
        {
            this.TimerName = config.Split('=')[0].Trim();

            var timeConfig = config.Split('=')[1].Trim();

            var regexHours = new Regex(@"(\d+)h");
            var hours =
                TimeSpan.FromHours(
                    regexHours.IsMatch(timeConfig) ? double.Parse(regexHours.Match(timeConfig).Groups[1].Value) : 0);

            var regexMinutes = new Regex(@"(\d+)m");
            var minutes =
                TimeSpan.FromMinutes(
                    regexMinutes.IsMatch(timeConfig) ? double.Parse(regexMinutes.Match(timeConfig).Groups[1].Value) : 0);

            this.TotalTime = hours + minutes;
        }

        private static string GetTimeStringOld(TimeSpan timeSpan, bool withSeconds = false)
        {
            var items = new List<string>();

            var hours = string.Format("{0}h", (int)Math.Floor(timeSpan.TotalHours));
            var minutes = string.Format("{0}m", timeSpan.Minutes);
            var seconds = string.Format("{0}s", timeSpan.Seconds);

            if (timeSpan.Hours != 0)
            {
                items.Add(hours);
            }

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
                    if (timeSpan.Seconds != 0)
                    {
                        items.Add(seconds);
                    }
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