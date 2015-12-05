using Donky.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickstartApp
{
    public class RichInboxTabbedPageLocalEvent : LocalEvent
    {
        public string Title { set; get; }
    }
}
