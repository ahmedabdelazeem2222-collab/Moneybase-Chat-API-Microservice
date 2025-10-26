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
        private IGenericRepository<ChatSession, Guid> _chats;
        private IGenericRepository<Agent, Guid> _agents;

        public UnitOfWork(MoneyBaseContext context)
        {
            _context = context;
        }

        public IGenericRepository<ChatSession, Guid> Chats
            => _chats ??= new GenericRepository<ChatSession, Guid>(_context);

        public IGenericRepository<Agent, Guid> Agents
            => _agents ??= new GenericRepository<Agent, Guid>(_context);

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
