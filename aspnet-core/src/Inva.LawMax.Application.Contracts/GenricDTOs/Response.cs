using System;
using System.Collections.Generic;
using System.Text;

namespace Inva.LawMax.GenricDTOs
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public bool IsSuccesded { get; set; }
        public List<Error>? errors { get; set; }

        public Response<T> CreateFailure(List<Error> errors)
        {
            this.IsSuccesded = false;
            this.errors = errors;
            return this;
        }

        public Response<T> CreateSuccess(T data)
        {
            this.IsSuccesded = true;
            this.Data = data;
            return this;
        }
    }

    public class Error
    {
        public string ErrorMessage { get; set; }
    }

    public class Empty { }
}
