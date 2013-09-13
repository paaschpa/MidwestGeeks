using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MidwestGeeks.Lib;

namespace MidwestGeeks.ServiceInterface.ApiRepositories
{
    public abstract class ApiRepositoryBase
    {
        protected abstract IEnumerable<TOutput> Deserialize<TOutput>(Stream stream);

        protected virtual IEnumerable<T> GetEvents<T>(string[] groupIds, Func<string, string> urlBuilder)
        {
            var events = new ConcurrentBag<T>();
            Parallel.ForEach(groupIds, groupId =>
                {
                    var request = WebRequest.Create(urlBuilder(groupId));
                    var response = request.GetResponse();

                    using (var stream = response.GetResponseStream())
                    {
                        var localEvents = Deserialize<T>(stream);
                        foreach (var ev in localEvents)
                        {
                            events.Add(ev);
                        }
                    }
                });

            return events;
        }
    }
}
