using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommence.Infrastructure.Http
{
    public class HttpResponseObjectError<T> : HttpResponseObject<T>
        where T : class
    {
        /// <summary>
        ///     Empty constructor
        /// </summary>
        public HttpResponseObjectError()
        {
            InitializeToBadRequest();
        }
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="item"></param>
        public HttpResponseObjectError(T item)
            : base(item)
        {
            InitializeToBadRequest();
        }
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="items"></param>
        public HttpResponseObjectError(IEnumerable<T> items)
            : base(items)
        {
            InitializeToBadRequest();
        }

        /// <summary>
        ///     Initializes Status and Message fields to Bad Request
        /// </summary>
        private void InitializeToBadRequest()
        {
            Status = 400;
            Message = "Bad Request";
        }
    }
}
