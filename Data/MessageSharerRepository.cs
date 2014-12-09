using MessageSharer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageSharer.Data
{
    public class MessageSharerRepository : IMessageSharerRepository
    {
        MessageSharerContext _ctx;
        public MessageSharerRepository(MessageSharerContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Topic> GetTopics()
        {
            return _ctx.Topics;
        }

        public IQueryable<Reply> GetRepliesByTopic(int topicId)
        {
            return _ctx.Replies.Where(r => r.TopicId == topicId);
        }


        public bool Save()
        {
            try
            {
               return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                //TODO log this error
                return false;
            }
        }

        public bool AddTopic(Topic newTopic)
        {
            try
            {
                _ctx.Topics.Add(newTopic);
                return true;
            }
            catch (Exception ex)
            {
                //TODO log this error
                return false;
            }
        }

        public bool AddContact(ContactModel newContact)
        {
            try
            {
                _ctx.Contacts.Add(newContact);
                return true;
            }
            catch (Exception ex)
            {
                //TODO log this error
                return false;
            }
        }


        public IQueryable<Topic> GetTopicsIncludingReplies()
        {
            return _ctx.Topics.Include("Replies");
        }


        public bool AddReply(Reply newReply)
        {
            try
            {
                _ctx.Replies.Add(newReply);
                return true;
            }
            catch (Exception ex)
            {
                //TODO log this error
                return false;
            }
        }
    }
}