using Assets.CodeBase.Services;
using CodeBase.Progress;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.Service
{
    public interface ISaveLoadService : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        void CleanupCode();
        void InformProgressReaders();
        void InformProgressWriters();
        void Register(ISavedProgress saved);
    }
}