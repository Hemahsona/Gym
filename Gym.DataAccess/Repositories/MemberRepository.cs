using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DataAccess.Repositories
{
    public class MemberRepository(GymDBContext dbContext) : Repository<Member>(dbContext), IMemberRepository
    {

    }
}
