using Donky.Messaging.Rich.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickstartApp.Models
{
    public class ActionSheetModel
    {
        public Guid RichMessageId { get; set; }
        public string[] Buttons { get; set; }
    }
}
