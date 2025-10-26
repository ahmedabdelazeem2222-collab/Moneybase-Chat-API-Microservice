using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MoneyBase.Support.Domain.Entities;
using MoneyBase.Support.Infrastructure.Persistence;
using MoneyBase.Support.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBase.UnitTests
{
    public class ChatRepositoryTests
    {
        /// <summary>
        /// EF Core InMemory to avoid a real database, FluentAssertions for readable assertions, xUnit as test runner
        /// </summary>
        /// <returns></returns>
        private MoneyBaseContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<MoneyBaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // unique DB each run
                .Options;

            return new MoneyBaseContext(options);
        }

        [Fact]
        public async Task CountPendingAsync_ShouldReturn_CorrectCount()
        {
            // Arrange
            var context = CreateInMemoryContext();

            context.ChatSessions.AddRange(
                new ChatSession { ChatStatus = Support.Domain.Enums.ChatStatusEnum.Pending },
                new ChatSession { ChatStatus = Support.Domain.Enums.ChatStatusEnum.Active }, 
                new ChatSession { ChatStatus = Support.Domain.Enums.ChatStatusEnum.Refused }
            );
            await context.SaveChangesAsync();

            var repository = new ChatRepository(context);

            // Act
            var count = await repository.CountPendingAsync();

            // Assert
            count.Should().Be(1); // only one pending
        }
    }
}
