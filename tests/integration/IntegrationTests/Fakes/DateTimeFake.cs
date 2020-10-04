using Application.Common.Interfaces;
using System;

namespace IntegrationTests.Fakes
{
    public class DateTimeFake : IDateTime
    {
        public DateTime FakedNow { private get; set; } = DateTime.Parse("2017-04-14T15:35:00");
        public DateTime Now => FakedNow;
    }
}