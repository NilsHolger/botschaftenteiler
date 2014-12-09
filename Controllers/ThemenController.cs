using MessageSharer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MessageSharer.Controllers
{
    public class ThemenController : ApiController
    {
        private IMessageSharerRepository _repo;
        public ThemenController(IMessageSharerRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<Topic> Get(bool mitAntworten = false)
        {
            IQueryable<Topic> results;

            if (mitAntworten == true)
            {
                results = _repo.GetTopicsIncludingReplies();
            }
            else
            {
                results = _repo.GetTopics();
            }

            var topics = results.OrderByDescending(t => t.Created).Take(50);
            return topics;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Topic newTopic)
        {
            if (newTopic.Created == default(DateTime))
            {
                newTopic.Created = DateTime.UtcNow;
            }
             if (_repo.AddTopic(newTopic) && _repo.Save())
             {
                 return Request.CreateResponse(HttpStatusCode.Created, newTopic);
             }
             return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
