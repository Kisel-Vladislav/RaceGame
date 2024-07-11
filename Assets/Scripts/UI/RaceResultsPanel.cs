using Agava.YandexGames;
using Assets.Scripts;
using CodeBase.Infrastructure.Factory;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class RaceResultsPanel : MonoBehaviour
    {
        [SerializeField] private TimeDisplay _bestTime;
        [SerializeField] private TimeDisplay _currentTime;
        [SerializeField] private TMP_Text _reward;

        [SerializeField] private LeaderboardDisplay _leaderboard;

        public void Construct(LevelId levelId) =>
            _leaderboard.Construct(levelId);

        public void Show(float currentTime, float bestLopTimeInSeconds, int reward)
        {
            _currentTime.Time = string.Format("{0:00.00}", currentTime);
            _bestTime.Time = string.Format("{0:00.00}", bestLopTimeInSeconds);
            _reward.text = reward.ToString();

            _leaderboard.Refresh();

            gameObject.SetActive(true);
        }
    }
}