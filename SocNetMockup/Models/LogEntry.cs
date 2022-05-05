using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SocNetMockup.Util;
using SocNetMockup.Util.Attributes;

namespace SocNetMockup.Models
{
    public class LogEntry
    {
        [NotMapped]
        public static readonly bool ValueCompressionEnabled = false;

        /// <summary>
        /// ID of an entry. Integer, auto incremented. Not exposed to the API.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Type of the log entry. Helps determine what schema to use.
        /// </summary>
        public LogEntryType Type { get; set; }

        /// <summary>
        /// JSON-encoded (maybe even compressed, see <see cref="ValueCompressionEnabled"/>) data specific to the type of the log entry.
        /// You should not use this directly unless really needed, please use
        /// </summary>
        public string Value { get; set; }

        public string EncodeValue<TSchema>(TSchema value) where TSchema : LogEntryValueSchemas.Schema
        {
            if (ValueCompressionEnabled) {
                return Compress(JsonConvert.SerializeObject(Value));
            }

            return JsonConvert.SerializeObject(Value);
        }

        public TSchema ParseValue<TSchema>() where TSchema : LogEntryValueSchemas.Schema
        {
            if (ValueCompressionEnabled) {
                return JsonConvert.DeserializeObject<TSchema>(Decompress(Value));
            }

            return JsonConvert.DeserializeObject<TSchema>(Value);
        }

        public string Compress(string decompressed) => throw new NotImplementedException("TODO: Implement compressing function");
        public string Decompress(string compressed) => throw new NotImplementedException("TODO: Implement decompressing function");
    }

    /// <summary>
    /// Entry of a log type. Documentation here also explains the schema of a log entry.
    /// </summary>
    public enum LogEntryType : short
    {
        MessageDelete,
        MessageEdit,
        UserKickedFromChat,
        UserAddedToChat,
        UserGivenRole,
        UserRevokedRole
    }

    public static class LogEntryValueSchemas
    {
        public class Schema { }

        public class MessageDeleteSchema : Schema
        {
            [GuidActuallyStandsFor(GuidMeaning.GroupChatMessageId)]
            public Guid MessageId { get; set; }

            [GuidActuallyStandsFor(GuidMeaning.GroupChatMemberId)]
            public Guid DeletedBy { get; set; }
        }

        public class MessageEditSchema : Schema
        {
            [GuidActuallyStandsFor(GuidMeaning.GroupChatMessageId)]
            public Guid MessageId { get; set; }

            public string OldValue { get; set; }

            [Obsolete("New value can be retrieved from either the next edit log entry or from the message itself, if this log entry is last."), NotMapped]
            public Uninstantiable NewValue { get; }
        }

        public class UserKickedFromChatSchema : Schema
        {
            [GuidActuallyStandsFor(GuidMeaning.GroupChatMemberId)]
            public Guid UserId { get; set; }
            
            [GuidActuallyStandsFor(GuidMeaning.GroupChatMemberId)]
            public Guid KickedBy { get; set; }
            
            public string Reason { get; set; }
        }

        public class UserAddedToChatSchema : Schema
        {
            [GuidActuallyStandsFor(GuidMeaning.GroupChatMemberId)]
            public Guid UserId { get; set; }
            
            [GuidActuallyStandsFor(GuidMeaning.GroupChatMemberId)]
            public Guid AddedBy { get; set; }
        }

        public class UserGivenRoleSchema : Schema
        {
            [GuidActuallyStandsFor(GuidMeaning.GroupChatMemberId)]
            public Guid UserId { get; set; }
            
            [GuidActuallyStandsFor(GuidMeaning.GroupChatMemberId)]
            public Guid GivenBy { get; set; }
            
            [GuidActuallyStandsFor(GuidMeaning.GroupChatRoleId)]
            public Guid RoleId { get; set; }
        }

        public class UserRevokedRoleSchema : Schema
        {
            [GuidActuallyStandsFor(GuidMeaning.GroupChatMemberId)]
            public Guid UserId { get; set; }
            
            [GuidActuallyStandsFor(GuidMeaning.GroupChatMemberId)]
            public Guid RevokedBy { get; set; }
            
            [GuidActuallyStandsFor(GuidMeaning.GroupChatRoleId)]
            public Guid RoleId { get; set; }
        }
    }
}
