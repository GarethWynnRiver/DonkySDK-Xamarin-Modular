using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Donky.Messaging.Rich.Inbox.XamarinForms.Converters
{
    public class ItemDateTimeDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            /*
                [14:24:33] Chris Wunsch: Less than 5 minutes = Just now
                [14:24:44 | Edited 14:24:45] Chris Wunsch: More than 5 minutes but less than 1 hour = X min ago
                [14:24:56] Chris Wunsch: more than 1 hour but still on the same day = X hours ago
                [14:25:08] Chris Wunsch: more than 1 hour but the previous day = yesterday
                [14:25:16] Chris Wunsch: more than 24 hours (i.e. yesterday) = yesterday
                [14:25:34] Chris Wunsch: more than 24 hours but less than 1 week ago = Name of day i.e. Tuesday
                [14:25:43] Chris Wunsch: more than 1 week ago = 22/09/2015             * */

            string response = string.Empty;

            var now = DateTime.UtcNow;
            var sent = (DateTime)value;

            int totalMinutesSinceSent = (int)Math.Ceiling((now - sent).TotalMinutes);
            int totalHoursSinceSent = (int)Math.Ceiling((now - sent).TotalHours);

            bool wasSentToday = ((sent.Date == now.Date) &&(sent.Month == now.Month) && (sent.Year == now.Year));
            bool wasSentYesterday = sent == DateTime.Now.AddDays(-1);

            bool wasSentGreaterThan1HourAgo = totalMinutesSinceSent > 60;
            bool wasSentGreaterThan24HoursAgo = (totalHoursSinceSent > 24);
            bool wasSentWithinLast7Days = DateTime.Now.Subtract(sent).Days <= 7;
            bool wasSentGreaterThanSevenDaysAgo = DateTime.Now.Subtract(sent).Days > 7;

            // Less than 5 minutes = Just now
            if (totalMinutesSinceSent < 5)
            {
                response = "Just now";
            }
            // More than 5 minutes but less than 1 hour = X min ago
            if ((totalMinutesSinceSent > 5) && (totalMinutesSinceSent < 60))
            {
                response = string.Format("{0} min ago", totalMinutesSinceSent.ToString());
            }
            // more than 1 hour but still on the same day = X hours ago
            if (wasSentGreaterThan1HourAgo && wasSentToday)
            {
                response = string.Format("{0} hours ago", totalHoursSinceSent.ToString());
            }
            // more than 1 hour but the previous day = yesterday    
            if (wasSentGreaterThan1HourAgo && wasSentYesterday)
            {
                response = "yesterday";
            }
            // more than 24 hours but less than 1 week ago = Name of day i.e. Tuesday
            if (wasSentGreaterThan24HoursAgo && wasSentWithinLast7Days)
            {
                response = (now - sent).ToString("dddd", new CultureInfo("en-GB"));
            }
            // more than 1 week ago = 22/09/2015
            if (wasSentWithinLast7Days)
            {
                response = string.Format("{0:dd/MM/yy}", sent);
            }
            
            return response;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
