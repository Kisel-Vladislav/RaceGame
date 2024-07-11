using CodeBase.Progress;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.Service
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IDataProvider _dataProvider;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public SaveLoadService(IPersistentProgressService persistentProgressService, IDataProvider dataProvider)
        {
            _persistentProgressService = persistentProgressService;
            _dataProvider = dataProvider;
        }

        public void InformProgressReaders()
        {
            foreach (var progressReader in ProgressReaders)
                progressReader.LoadProgress(_persistentProgressService.PlayerProgress);
        }
        public void InformProgressWriters()
        {
            foreach (var progressWriter in ProgressWriters)
                progressWriter.SaveProgress(_persistentProgressService.PlayerProgress);

            _dataProvider.Save();
        }
        public void Register(ISavedProgress saved)
        {
            if(saved is ISavedProgressReader reader)
                ProgressReaders.Add(reader);

            ProgressWriters.Add(saved);
        }
        public void CleanupCode()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}
