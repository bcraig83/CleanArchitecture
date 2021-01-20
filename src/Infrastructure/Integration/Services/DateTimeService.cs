using Application.Common.Interfaces;
using System;

namespace Integration.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}