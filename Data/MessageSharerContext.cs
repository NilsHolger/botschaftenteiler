using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MessageSharer.Models;

namespace MessageSharer.Data
{
    public class MessageSharerContext : DbContext
    {
        public MessageSharerContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<MessageSharerContext, 
                MessageSharerMigrationsConfiguration>()
                );
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<ContactModel> Contacts { get; set; }
    }
}