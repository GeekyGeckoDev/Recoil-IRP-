using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.Responses
{
    public class ApiResponseDto<T>
    {
        public bool Succes { get; set; }
        public string? Message { get; set; }
        public T? Result { get; set; }
    }
}
