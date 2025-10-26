using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MoneyBase.Support.Domain.Entities;
using MoneyBase.Support.Infrastructure.Persistence;
using MoneyBase.Support.Infrastructure.Persistence.Repositories;
using System;

namespace MoneyBase.UnitTests
{
    public class ChatRepository_GetActiveSessionsTests
    {
        private MoneyBaseContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<MoneyBaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new MoneyBaseContext(options);
        }

    }
}
