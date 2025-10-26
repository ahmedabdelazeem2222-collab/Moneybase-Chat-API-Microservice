using Microsoft.EntityFrameworkCore;
using MoneyBase.Support.Domain.Entities;
using MoneyBase.Support.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBase.Support.Infrastructure.Persistence
{
    public class DbInitializer
    {
        public static async Task SeedAsync(MoneyBaseContext ctx)
        {
        //    if (await ctx.Agents.AnyAsync()) return;

        //    var agents = new List<Agent>
        //{
        //    new Agent { Name = "A-TeamLead", Seniority = SeniorityEnum.TeamLead, ShiftStartHour = 8, ShiftEndHour = 16 },
        //    new Agent { Name = "A-Mid-1", Seniority = SeniorityEnum.Mid, ShiftStartHour = 8, ShiftEndHour = 16 },
        //    new Agent { Name = "A-Mid-2", Seniority = SeniorityEnum.Mid, ShiftStartHour = 8, ShiftEndHour = 16 },
        //    new Agent { Name = "A-Junior", Seniority = SeniorityEnum.Junior, ShiftStartHour = 8, ShiftEndHour = 16 },
        //    new Agent { Name = "B-Senior", Seniority = SeniorityEnum.Senior, ShiftStartHour = 16, ShiftEndHour = 24 },
        //    new Agent { Name = "B-Mid", Seniority = SeniorityEnum.Mid, ShiftStartHour = 16, ShiftEndHour = 24 },
        //    new Agent { Name = "B-Jnr-1", Seniority = SeniorityEnum.Junior, ShiftStartHour = 16, ShiftEndHour = 24 },
        //    new Agent { Name = "C-Mid-1", Seniority = SeniorityEnum.Mid, ShiftStartHour = 0, ShiftEndHour = 8 },
        //};

        //    await ctx.Agents.AddRangeAsync(agents);
        //    await ctx.SaveChangesAsync();
        }
    }
}
