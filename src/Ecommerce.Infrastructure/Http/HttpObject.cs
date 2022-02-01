using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommence.Infrastructure.Http
{
    public abstract class HttpObject<T>
       where T : class
    {
        public List<T> Items { get; set; } = new List<T>();

        /// <summary>
        ///     Constructor
        /// </summary>
        protected HttpObject()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="item"></param>
        protected HttpObject(T item)
        {
            if (null != item)
            {
                Items.Add(item);
            }
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="items"></param>
        protected HttpObject(IEnumerable<T> items)
        {
            if (null != items)
            {
                Items.AddRange(items);
            }
        }
    }
}
