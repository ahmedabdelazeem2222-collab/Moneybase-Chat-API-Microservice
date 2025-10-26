using MoneyBase.Support.Application.DTOs;
using MoneyBase.Support.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBase.Support.Application.Interfaces
{
    public interface IChatProducer
    {
        Task PublishAsync(ChatRequestDto request, CancellationToken ct = default);
    }
}
