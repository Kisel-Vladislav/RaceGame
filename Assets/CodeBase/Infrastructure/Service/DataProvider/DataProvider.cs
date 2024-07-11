using CodeBase.Progress;
using System;

namespace CodeBase.Infrastructure.Service
{
    public abstract class DataProvider : IDataProvider
    {
        protected readonly IPersistentProgressService _persistentProgressService;

        public DataProvider(IPersistentProgressService persistentProgressService) =>
            _persistentProgressService = persistentProgressService;

        public virtual void LoadOrInitNew() =>
            _persistentProgressService.PlayerProgress = Load() ?? NewProgress();
        public virtual void Save() =>
            throw new NotImplementedException();

        protected virtual PlayerProgress Load() =>
            throw new NotImplementedException();
        private PlayerProgress NewProgress() =>
            new PlayerProgress();

    }
}
