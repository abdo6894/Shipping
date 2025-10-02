using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class DataAccessException : Exception
    {
        public DataAccessException(Exception ex,string CustomMessage, ILogger logger)
        {
            logger.LogError($"Main Exception Is {ex} Developer CustomException is +" +
                $"{CustomMessage}");
        }
    }
}
