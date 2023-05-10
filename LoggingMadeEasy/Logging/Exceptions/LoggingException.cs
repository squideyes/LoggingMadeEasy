// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace LoggingMadeEasy;

internal class LoggingException : Exception
{
    public LoggingException(string message, Exception innerException = null!) 
        : base(message, innerException) 
    { 
    }
}