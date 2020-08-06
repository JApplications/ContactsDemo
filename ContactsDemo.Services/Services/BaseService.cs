using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsDemo.Services.Services
{
    public class BaseService
    {
        protected readonly ILogger _logger;

        public BaseService(ILogger logger)
        {
            _logger = logger;
        }
    }
}
