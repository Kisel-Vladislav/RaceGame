using System.Collections.Generic;

namespace CodeBase.Infrastructure.Service.PausedService
{
    public class PausedService : IPausedService
    {
        public List<IPaused> Paused { get; } = new List<IPaused>();

        public void CleanupCode() =>
            Paused.Clear();
        public void Register(IPaused paused) =>
            Paused.Add(paused);
        public void SetPaused(bool isPaused)
        {
            foreach (var item in Paused)
                item.SetPaused(isPaused);
        }
    }
}