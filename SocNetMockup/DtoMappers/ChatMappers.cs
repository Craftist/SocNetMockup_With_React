using System.Linq;
using SocNetMockup.Dtos.Chat;
using SocNetMockup.Models.Chat;

namespace SocNetMockup.DtoMappers
{
    public static class ChatMappers
    {
        public static readonly Mapper<GroupChat, GroupChatDto> Chat = new(chat => new() {
            Id = chat.Id,
            Owner = Member.Map(chat.Members.FirstOrDefault(x => x.Id == chat.OwnerId)),
            Title = chat.Title,
            CreationDate = chat.CreationDate
        });
        
        public static readonly Mapper<GroupChat, GroupChatWithMembersDto> ChatWithMembers = new(chat => new() {
            Id = chat.Id,
            Owner = Member.Map(chat.Members.FirstOrDefault(x => x.Id == chat.OwnerId)),
            Title = chat.Title,
            CreationDate = chat.CreationDate,
            Members = Member.Map(chat.Members)
        });
        
        public static readonly Mapper<GroupChatMember, GroupChatMemberDto> Member = new(member => new() {
            Id = member.User.Id,
            UserName = member.User.UserName
        });
    }
}
