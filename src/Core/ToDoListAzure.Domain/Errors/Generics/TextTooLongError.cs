using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentResults;

namespace TodoListAzure.Domain.Errors.Generics
{
    public class TextTooLongError : Error
    {
        public TextTooLongError(string propertyName, int maxLength)
            : base($"{propertyName} should not be longuer than {maxLength}")
        {
        }
    }
}
