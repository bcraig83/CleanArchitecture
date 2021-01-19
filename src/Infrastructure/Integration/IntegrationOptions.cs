﻿namespace Integration
{
    public class IntegrationOptions
    {
        public const string AppSettingsFileLocation = "Infrastructure:Integration";

        public bool UseStaticDateTimeService { get; set; } = false;

        public override string ToString()
        {
            return $"{{{nameof(UseStaticDateTimeService)}={UseStaticDateTimeService}}}";
        }
    }
}