using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace MessageSharer.Data
{
    public class MessageSharerMigrationsConfiguration : DbMigrationsConfiguration<MessageSharerContext>
    {

        public MessageSharerMigrationsConfiguration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MessageSharerContext context)
        {
            base.Seed(context);
#if DEBUG
            if (context.Topics.Count() == 0)
            {
                var topic = new Topic()
                {
                    Title = "AngularJS ist spitze!",
                    Created = DateTime.Now,
                    Body = "AngularJS ist ein Helden Framework",
                    Replies = new List<Reply>()
          {
            new Reply()
            {
               Body = "Finde ich auch super!",
               Created = DateTime.Now
            },
            new Reply()
            {
               Body = "Total geil",
               Created = DateTime.Now
            },
            new Reply()
            {
               Body = "Ist ein Drecksding",
               Created = DateTime.Now
            },
          }
                };

                context.Topics.Add(topic);

                var anotherTopic = new Topic()
                {
                    Title = "Ruby ist spitze!",
                    Created = DateTime.Now,
                    Body = "Ruby on Rails ist beliebt"
                };

                context.Topics.Add(anotherTopic);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }
#endif
        }

    }
}
