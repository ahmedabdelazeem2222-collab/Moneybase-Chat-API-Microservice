using MoneyBase.Support.Application.Interfaces;
using MoneyBase.Support.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBase.Support.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MoneyBaseContext _context;

        public IChatRepository Chats { get; }
        public IAgentRepository Agents { get; }
        public UnitOfWork(MoneyBaseContext context)
        {
            Chats = new ChatRepository(_context);
            Agents = new AgentRepository(_context);
            _context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
