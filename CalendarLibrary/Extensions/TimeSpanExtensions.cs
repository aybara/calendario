using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarLibrary.Extensions
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan Min(TimeSpan timeSpan1, TimeSpan timeSpan2)
        {
            if (timeSpan1 <= timeSpan2)
                return timeSpan1;
            return timeSpan2;
        }
    }
}
