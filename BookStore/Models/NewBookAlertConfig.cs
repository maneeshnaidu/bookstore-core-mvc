using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class NewBookAlertConfig
    {
        public bool ToAlert { get; set; }
        public string AlertMessage { get; set; }
    }
}
