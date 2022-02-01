using System.Collections.Generic;


namespace Ecommence.Infrastructure.Http
{
    public class HttpResponseObjectSuccess<T> : HttpResponseObject<T>
       where T : class
    {
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 1;

        /// <summary>
        ///     Empty constructor
        /// </summary>
        public HttpResponseObjectSuccess()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="item"></param>
        public HttpResponseObjectSuccess(T item)
            : base(item)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="items"></param>
        public HttpResponseObjectSuccess(IEnumerable<T> items)
            : base(items)
        {
        }
    }
}
