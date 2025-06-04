using pilgrimsvandringen_grupp_5_e.Models;

namespace pilgrimsvandringen_grupp_5_e.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        void DeleteMessage(Message message);
        Task<IEnumerable<Message>> GetMessagesAsync();        
        Task PostMessageAsync(Message message);
        Task<User> GetUserByIdAsync(int id);
        Task<bool> CheckMessageTypeExistensAsync(string typeName);
        Task SeedMessageTypesAsync(MessageType messageType);
    }
}
