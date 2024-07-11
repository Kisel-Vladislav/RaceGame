using CodeBase.Progress;
using Data;
using UnityEngine;

namespace CodeBase.Infrastructure.Service
{
    public class LocalDataProvider : DataProvider
    {
        private const string ProgressKey = "ProgressKey";

        public LocalDataProvider(IPersistentProgressService persistentProgressService) : base(persistentProgressService)
        {
        }

        public override void Save() =>
            PlayerPrefs.SetString(ProgressKey, _persistentProgressService.PlayerProgress.ToJson());

        protected override PlayerProgress Load() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();

    }
}
