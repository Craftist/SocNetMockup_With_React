using System.Linq;
using SocNetMockup.Dtos.Chat;
using SocNetMockup.Models.Chat;

namespace SocNetMockup.DtoMappers
{
    public static class ChatMappers
    {
        public static readonly Mapper<GroupChat, GroupChatDto> Chat = new(chat => new() {
            Id = chat.Id,
            Owner = Member.Map(chat.Members.First(x => x.Id == chat.OwnerId)),
            Title = chat.Title
        });
        
        public static readonly Mapper<GroupChat, GroupChatWithMembersDto> ChatWithMembers = new(chat => new() {
            Id = chat.Id,
            Owner = Member.Map(chat.Members.First(x => x.Id == chat.OwnerId)),
            Title = chat.Title,
            Members = Member.Map(chat.Members)
        });
        
        public static readonly Mapper<GroupChatMember, GroupChatMemberDto> Member = new(member => new() {
            Id = member.User.Id,
            UserName = member.User.UserName
        });
    }
}
