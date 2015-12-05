using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickstartApp.Exceptions
{
    public class RandomCustomMessageNotificationJsonDeserialisationException : Exception
    {
        public RandomCustomMessageNotificationJsonDeserialisationException(string errorMessage, string innerExceptionMessage) : base(string.Format("RandomCustomMessageNotificationJsonDeserialisationException Exception: {0}. Inner Exception Message: {1}. [QuickstartApp.Exceptions.DeserialiseRandomCustomMessageNotificationJsonException]", errorMessage, innerExceptionMessage)) { }
    }
}
