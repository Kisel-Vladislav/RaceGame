using Assets.CodeBase.Services;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.Service.PausedService
{
    public interface IPausedService : IService
    {
        List<IPaused> Paused { get; }
        void Register(IPaused paused);
        void CleanupCode();
        void SetPaused(bool isPaused);
    }
}