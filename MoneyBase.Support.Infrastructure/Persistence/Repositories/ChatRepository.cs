using Microsoft.EntityFrameworkCore;
using MoneyBase.Support.Application.Interfaces;
using MoneyBase.Support.Domain.Entities;
using MoneyBase.Support.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBase.Support.Infrastructure.Persistence.Repositories
{
    public class ChatRepository : GenericRepository<ChatSession, Guid>, IChatRepository
    {
        #region Fields
        public ChatRepository(MoneyBaseContext context) : base(context) { }

        #endregion

        #region Methods
        public async Task<int> CountPendingAsync()
        {
            return await _dbSet
                .Where(s => s.ChatStatus == Domain.Enums.ChatStatusEnum.Pending)
                .CountAsync();
        }
        public async Task<List<ChatSession>> GetActiveSessionsAsync()
        {
            return await _dbSet.Where(s => s.ChatStatus == Domain.Enums.ChatStatusEnum.Active).ToListAsync();
        }


        #endregion
    }
}
