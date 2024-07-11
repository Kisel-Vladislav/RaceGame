using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class RacePanel : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private LoadLevelButton _loadLevelButton;

        public event Action OnClosed;

        private void OnEnable() => 
            _backButton.onClick.AddListener(OnBackButtonClick);
        private void OnDisable() => 
            _backButton.onClick.RemoveListener(OnBackButtonClick);

        public void Show() =>
            gameObject.SetActive(true);
        public void Hide() =>
            gameObject.SetActive(false);

        private void OnBackButtonClick()
        {
            Hide();
            OnClosed?.Invoke();
        }

    }
}