using pilgrimsvandringen_grupp_5_e.Models;

namespace pilgrimsvandringen_grupp_5_e.Services.Interfaces
{
    public interface IMessageService
    {
        Task AddMessageAsync(Message message);
        Task<IEnumerable<Message>> GetAllMessagesAsync();        
        Task<User> GetUserByIdAsync(int id);
        Task<bool> CheckMessageTypeExistensAsync(string typeName);
        Task SeedMessageTypesAsync(MessageType messageType);
    }
}
