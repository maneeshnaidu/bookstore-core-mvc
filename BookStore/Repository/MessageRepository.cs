using BookStore.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IOptionsMonitor<NewBookAlertConfig> _newBookAlertConfiguration;

        public MessageRepository(IOptionsMonitor<NewBookAlertConfig> newBookAlertConfiguration)
        {
            _newBookAlertConfiguration = newBookAlertConfiguration;
        }
        public string GetName()
        {
            return _newBookAlertConfiguration.CurrentValue.AlertMessage;
        }
    }
}
