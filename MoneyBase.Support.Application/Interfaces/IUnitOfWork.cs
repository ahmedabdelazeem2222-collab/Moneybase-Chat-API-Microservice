using MoneyBase.Support.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBase.Support.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<ChatSession, Guid> Chats { get; }
        IGenericRepository<Agent, Guid> Agents { get; }
        Task<int> CommitAsync();
    }
}
