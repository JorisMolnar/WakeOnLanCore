﻿using System;

namespace WakeOnLanCore.Exceptions
{
    /// <summary>
    /// The exception that is thrown when trying to add a duplicate object to a collection that does not allow duplicate objects.
    /// </summary>
    public class ObjectNotUniqueException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNotUniqueException"/> class.
        /// </summary>
        public ObjectNotUniqueException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNotUniqueException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ObjectNotUniqueException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNotUniqueException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ObjectNotUniqueException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
