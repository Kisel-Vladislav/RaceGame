using CodeBase.Car;
using CodeBase.Infrastructure.Service.Wallet;
using CodeBase.Progress;
using CodeBase.StaticData;
using Scripts.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Shop
{
    public class CarShop : Shop, ISavedProgressReader
    {
        [Header("Buttons")]
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _buyButton;

        [Header("Text")]
        [SerializeField] private TMP_Text _price;

        [Header("View")]
        [SerializeField] private ItemPlacement _skinPlacement;
        [SerializeField] private CarStatsPanel StatsPanel;
        [SerializeField] private MoneyDisplay _moneyDisplay;

        [Header("Data")]
        [SerializeField] private CarShopContent _shopContent;


        private List<CarTypeId> _boughtCar = new List<CarTypeId>();
        private CarTypeId _selectCar;
        private CarShopItem PreviewedCar => _shopContent.items[_previewedItemIndex];
        private int _previewedItemIndex = 0;

        private IWallet _wallet;
        private IStaticDataService _staticDataService;

        public void Construct(IWallet wallet, IStaticDataService staticDataService)
        {
            _moneyDisplay.Construct(wallet);

            _wallet = wallet;
            _staticDataService = staticDataService;
        }
        private void OnEnable()
        {
            ShopEnable();

            _previousButton.onClick.AddListener(OnPreviousButtonClick);
            _nextButton.onClick.AddListener(OnNextButtonClick);
            _buyButton.onClick.AddListener(OnBuyButtonClick);
            _selectButton.onClick.AddListener(OnSelectButtonClick);

            _skinPlacement.InstantiateModel(PreviewedCar.Model);
        }
        private void OnDisable()
        {
            ShopDisable();

            _previousButton.onClick.RemoveListener(OnPreviousButtonClick);
            _nextButton.onClick.RemoveListener(OnNextButtonClick);
            _buyButton.onClick.RemoveListener(OnBuyButtonClick);
            _selectButton.onClick.RemoveListener(OnSelectButtonClick);
        }

        private void InitializeShopItem() =>
            UpdatePreviewedItem(_previewedItemIndex);

        private void OnSelectButtonClick()
        {
            _selectCar = PreviewedCar.CarTypeId;
            HideButtons();
        }
        private void OnBuyButtonClick()
        {
            if (_wallet.TrySpend(PreviewedCar.Price))
            {
                ShowSelectButton();
                _boughtCar.Add(PreviewedCar.CarTypeId);
            }
        }
        private void OnPreviousButtonClick() =>
            UpdatePreviewedItem(_previewedItemIndex - 1 >= 0 ? _previewedItemIndex - 1 : _shopContent.items.Count - 1);
        private void OnNextButtonClick() =>
            UpdatePreviewedItem((_previewedItemIndex + 1) % _shopContent.items.Count);

        private void InstanceModel() =>
            _skinPlacement.InstantiateModel(PreviewedCar.Model);
        private void UpdatePreviewedItem(int nextItemIndex)
        {
            _previewedItemIndex = nextItemIndex;
            RefreshStatsPanel();
            UpdatePriceDisplay();
            InstanceModel();

            if (_selectCar == PreviewedCar.CarTypeId)
                HideButtons();
            else if (_boughtCar.Contains(PreviewedCar.CarTypeId))
                ShowSelectButton();
            else
                ShowBuyButton();
        }
        private void UpdatePriceDisplay() =>
            _price.text = PreviewedCar.Price.ToString();
        private void RefreshStatsPanel() =>
            StatsPanel.Refresh(_staticDataService.ForCar(PreviewedCar.CarTypeId));
        private void ShowBuyButton()
        {
            HideButton(_selectButton);
            ShowButton(_buyButton);
        }
        private void HideButtons()
        {
            HideButton(_buyButton);
            HideButton(_selectButton);
        }
        private void ShowSelectButton()
        {
            HideButton(_buyButton);
            ShowButton(_selectButton);
        }

        private void HideButton(Button button) => button.gameObject.SetActive(false);
        private void ShowButton(Button button) => button.gameObject.SetActive(true);

        public void LoadProgress(PlayerProgress progress)
        {
            _selectCar = progress.PlayerCars.CurrentCar;
            _boughtCar = progress.PlayerCars.AllCars;

            InitializeShopItem();
        }
        public void SaveProgress(PlayerProgress progress)
        {
            progress.PlayerCars.CurrentCar = _selectCar;
            progress.PlayerCars.AllCars = _boughtCar;
        }
    }
}