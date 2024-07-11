using Agava.YandexGames;
using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace Assets.Scripts
{
    public class LeaderboardDisplay : MonoBehaviour
    {
        private const string DefaultName = "Anonymous";

        private LevelId _levelId;

        public LeaderboardEntryView[] leaderboardEntryView;
        public void Construct(LevelId levelId) =>
            _levelId = levelId;

        public void Refresh()
        {
            if (!YandexGamesSdk.IsRunningOnYandex)
                return;

            Leaderboard.GetEntries(_levelId.ToString(), (result) =>
            {
                for (var i = 0; i < leaderboardEntryView.Length && i < result.entries.Length; i++)
                {
                    var entry = result.entries[i];
                    string name = entry.player.publicName;
                    if (string.IsNullOrEmpty(name))
                        name = DefaultName;

                    leaderboardEntryView[i].Construct(name, entry.score);
                }
            });
        }

    }
}