﻿using System;
using System.Linq;
using System.Reflection;

namespace SimpleExtension.Core
{
    /// <summary>
    ///     Class ExceptionExtension.
    /// </summary>
    public static class ExceptionExtension
    {
        /// <summary>
        ///     Formats for human.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>System.String.</returns>
        public static string FormatForHuman(this Exception exception)
        {
            var properties = exception.GetType()
                .GetProperties();
            var fields = properties
                .Select(property => new
                {
                    property.Name,
                    Value = property.GetValue(exception, null)
                })
                .Select(x => $"{x.Name} = {x.Value?.ToString() ?? string.Empty}");
            return string.Join("\n", fields as string[]);
        }
    }
}