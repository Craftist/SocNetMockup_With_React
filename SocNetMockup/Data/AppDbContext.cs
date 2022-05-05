using System;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SocNetMockup.Data.ValueConverters;
using SocNetMockup.Models;
using SocNetMockup.Models.Messenger;

namespace SocNetMockup.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>, IPersistedGrantDbContext
    {
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

        public AppDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options)
        {
            _operationalStoreOptions = operationalStoreOptions;
        }

        Task<int> IPersistedGrantDbContext.SaveChangesAsync() => base.SaveChangesAsync();
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        public DbSet<GroupChat> GroupChats { get; set; }
        public DbSet<GroupChatMember> GroupChatMembers { get; set; }
        public DbSet<Message> GroupChatMessages { get; set; }

        public DbSet<PmPeer> PmPeers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);

            var gcBuilder = builder.Entity<GroupChat>();
            
            gcBuilder.HasKey(x => x.Id);
            gcBuilder.HasMany(x => x.Members).WithOne(x => x.Chat).IsRequired();

            var gcMemberBuilder = builder.Entity<GroupChatMember>();
            gcMemberBuilder.HasKey(x => x.Id);
            gcMemberBuilder.HasOne(x => x.User).WithMany(x => x.GroupChatMemberships).IsRequired();

            var messageBuilder = builder.Entity<Message>();
            messageBuilder.HasKey(x => x.Id);
            messageBuilder.HasOne(x => x.Peer).WithMany(x => x.Messages);
            messageBuilder.HasOne(x => x.Chat).WithMany(x => x.Messages);

            var imageBuilder = builder.Entity<Image>();
            imageBuilder.HasKey(x => x.Id);
            //imageBuilder.Property(x => x.VisibleIn).HasConversion<GuidListToStringValueConverter>();

            var pmPeerBuilder = builder.Entity<PmPeer>();
            pmPeerBuilder.HasKey(x => x.Id);
            pmPeerBuilder.HasMany(x => x.Members).WithMany(x => x.Peers);
        }
    }
}
