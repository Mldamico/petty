using Petty.DTO;
using Petty.Entities;
using Petty.Helpers;

namespace Petty.Interfaces;

public interface IMessageRepository
{
    void AddMessage(Message message);
    void DeleteMessage(Message message);
    Task<Message> GetMessage(int id);
    Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
    Task<IEnumerable<MemberDto>> GetMessageThread(int currentUserId, int recipientId);
    Task<bool> SaveAllAsync();
}