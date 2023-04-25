﻿using Web_API.Models;

namespace Web_API.Repository.Contracts
{
    public interface IProfilingRepository : IGeneralRepository<Profiling, int>
    {
        Task<dynamic> GetTotalbyMajor();
        Task<dynamic> GetWorkPeriod();
    }
}
