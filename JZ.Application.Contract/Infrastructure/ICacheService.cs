using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Application.Contract.Infrastructure
{
    public interface ICacheService
    {
        void Set(string key, string value, TimeSpan? slidingExpiration = null, DateTimeOffset? absoluteExpiration = null);
        void Set<T>(string key, T value, TimeSpan? slidingExpiration = null, DateTimeOffset? absoluteExpiration = null);

        string Get(string key);

        T Get<T>(string key);

        void Remove(string key);
        bool Contains(string key);
    }
}
