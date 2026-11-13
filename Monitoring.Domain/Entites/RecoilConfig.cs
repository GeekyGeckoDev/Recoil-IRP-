using Monitoring.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Domain.Entites
{
    public static class RecoilConfigModel
    {
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static string[] Blacklist { get; set; }
    }

    public class RecoilSettings
    {
        public bool IsAutoClear { get; set; }
        public AutoClearSchedule ClearTimeSchedule { get; set; } = AutoClearSchedule.Weekly;
        public string SetExternalDbConnString { get; set; } = string.Empty;
    }

    public static class RecoilExternalDbConfig
    {
        public static string ConnectionString { get; set; } = string.Empty;
        public static string DbName { get; set; } = "RecoilDb";
    }

    public static class AutoClearModel
    {
        public static bool IsAutoClear { get; set; }
        public static AutoClearSchedule ClearTimeSchedule { get; set; } = AutoClearSchedule.Weekly;
    }
}
