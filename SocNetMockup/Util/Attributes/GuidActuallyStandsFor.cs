using System;

namespace SocNetMockup.Util.Attributes
{
    public class GuidActuallyStandsForAttribute : Attribute
    {
        public GuidActuallyStandsForAttribute(GuidMeaning guidMeaning) { }
    }

    public enum GuidMeaning
    {
        /// <summary>
        /// Any Guid that doesn't fall under other categories in this enum.
        /// </summary>
        RegularGuid,
        
        /// <summary>
        /// Global app <see cref="SocNetMockup.Models.User"/> ID.
        /// </summary>
        UserId,
        
        /// <summary>
        /// <see cref="SocNetMockup.Models.Messenger.GroupChat"/> ID.
        /// </summary>
        GroupChatId,
        
        /// <summary>
        /// <see cref="SocNetMockup.Models.Messenger.GroupChatMember"/> ID.
        /// </summary>
        GroupChatMemberId,
        
        /// <summary>
        /// <see cref="SocNetMockup.Models.Messenger.GroupChatChannel"/> ID.
        /// </summary>
        GroupChatChannelId,
        
        /// <summary>
        /// <see cref="SocNetMockup.Models.Messenger.GroupChatMessage"/> ID.
        /// </summary>
        GroupChatMessageId,
        
        /// <summary>
        /// <see cref="SocNetMockup.Models.Messenger.GroupChatRole"/> ID.
        /// </summary>
        GroupChatRoleId,
        
        /// <summary>
        /// <see cref="SocNetMockup.Models.Image"/> ID.
        /// </summary>
        ImageId,
    }
}
