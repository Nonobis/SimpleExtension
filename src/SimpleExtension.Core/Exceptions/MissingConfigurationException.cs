using System.Runtime.Serialization;
using System;

namespace SimpleExtension.Core.Exceptions
{
    public class MissingConfigurationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingConfigurationException" /> class.
        /// </summary>
        public MissingConfigurationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingConfigurationException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MissingConfigurationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingConfigurationException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public MissingConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingConfigurationException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected MissingConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
