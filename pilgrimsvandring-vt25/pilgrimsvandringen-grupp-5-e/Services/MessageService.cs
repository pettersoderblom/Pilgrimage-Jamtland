using Microsoft.EntityFrameworkCore;
using pilgrimsvandringen_grupp_5_e.Models;
using pilgrimsvandringen_grupp_5_e.Repositories.Interfaces;
using pilgrimsvandringen_grupp_5_e.Services.Interfaces;
using System.Runtime.CompilerServices;

namespace pilgrimsvandringen_grupp_5_e.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository) 
        { 
            _messageRepository = messageRepository;
        }

        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            var result = await _messageRepository.GetMessagesAsync();
            return result;
        }

        public async Task AddMessageAsync(Message message)
        {
            await _messageRepository.PostMessageAsync(message);

        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _messageRepository.GetUserByIdAsync(id);
            return user;
        }

        public async Task SeedMessageTypesAsync(MessageType messageType)
        {
            await _messageRepository.SeedMessageTypesAsync(messageType);
        }

        public async Task<bool> CheckMessageTypeExistensAsync(string typeName)
        {
            var result = await _messageRepository.CheckMessageTypeExistensAsync(typeName);
            if(result != null && result == true)
            {
                return true;
            }
            return false;
        }
    }
}
