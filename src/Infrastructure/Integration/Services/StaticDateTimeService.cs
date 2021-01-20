using Application.Common.Interfaces;
using System;

namespace Integration.Services
{
    // This class is obviously useless. It's just put in here
    // as an example of how the IntegrationOptions might be used.
    public class StaticDateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Parse("2021-01-18 15:35:41");
    }
}