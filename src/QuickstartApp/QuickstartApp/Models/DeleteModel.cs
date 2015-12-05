using Donky.Messaging.Rich.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickstartApp.Models
{
    public class DeleteModel
    {
        public bool DeleteAll { get; set; }
        public RichMessage Message { get; set; }
    }
}
