using System;
using System.Collections.Generic;

namespace ServiceLayer
{
    [Serializable]
    public class ServiceResponse<T> : IServiceResponse<T>
    {
        public ServiceResponse() { }
        public IList<T> List { get; set; }

        public T Entity { get; set; }

        public int Count { get; set; }

        public bool IsSuccessful { get; set; }
    }
}
