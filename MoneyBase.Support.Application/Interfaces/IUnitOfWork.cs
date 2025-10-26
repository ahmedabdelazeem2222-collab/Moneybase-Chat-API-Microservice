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
        IChatRepository Chats { get; }
        IAgentRepository Agents { get; }
        Task<int> CommitAsync();
    }
}
