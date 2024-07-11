using CodeBase.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _playButton;

        [Header("Panels")]
        public CarShop Shop;
        public RacePanel RacePanel;

        private void OnEnable()
        {
            _shopButton.onClick.AddListener(OnShopButtonClick);
            _playButton.onClick.AddListener(OnPlayButtonClick);

            Shop.Closed -= Show;
            RacePanel.OnClosed -= Show;
        }
        private void OnDisable()
        {
            _shopButton.onClick.RemoveListener(OnShopButtonClick);
            _playButton.onClick.RemoveListener(OnPlayButtonClick);

            Shop.Closed += Show;
            RacePanel.OnClosed += Show;
        }

        private void OnShopButtonClick()
        {
            Hide();
            Shop.Show();
        }
        private void OnPlayButtonClick()
        {
            Hide();
            RacePanel.Show();
        }
        private void Show() =>
            gameObject.SetActive(true);
        private void Hide() =>
            gameObject.SetActive(false);
    }
}