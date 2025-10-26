using MoneyBase.Support.Application.Interfaces;
using MoneyBase.Support.Domain.Entities;
using System;

namespace MoneyBase.Support.Infrastructure.Persistence.Repositories
{
    public class AgentRepository : GenericRepository<Agent, Guid>, IAgentRepository
    {
        #region Fields
        public AgentRepository(MoneyBaseContext context) : base(context) { }

        #endregion

        #region Methods

        #endregion
    }
}
