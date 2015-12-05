using Com.Xamtastic.Patterns.SmallestMvvm;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuickstartApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickstartApp.ViewModels.Notifications
{
    public class RandomCustomMessageNotificationViewModel : ViewModelBase
    {
        public RandomCustomMessageNotificationViewModel() { }

        public RandomCustomMessageNotificationViewModel(string json)
        {
            try
            {
                var jo = JObject.Parse(json);

                foreach (var item in jo)
                {
                    if (item.Key.Equals("customData"))
                    {
                        Debug.WriteLine("Message Item: " + item.Value);

                        var obj = JsonConvert.DeserializeObject<RandomCustomMessageNotificationViewModel>(item.Value.ToString());

                        this.customId = obj.customId;
                        this.to = obj.to;
                        this.from = obj.from;
                        this.message = obj.message;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RandomCustomMessageNotificationJsonDeserialisationException(ex.Message, ex.InnerException.Message);
            }
        }

        private string _customId;
        public string customId
        {
            get
            {
                return _customId.Substring(0,8);
            }
            set
            {
                _customId = value;
            }
        }

        private string _to;
        public string to
        {
            get
            {
                return _to;
            }
            set
            {
                _to = value;
            }
        }

        private string _from;
        public string from
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
            }
        }

        private string _message;
        public string message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }
    }
}
