using CodeBase.StaticData;
using TMPro;
using UnityEngine;

namespace CodeBase.Shop
{
    public class CarShopItemView : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField] private TMP_Text _price;

        private IStaticDataService _staticDataService;

        public CarShopItem Item;
        public CarStatsPanel StatsPanel;

        public int Price => Item.Price;

        public void Construct(IStaticDataService staticDataService, CarShopItem carShopItem)
        {
            _staticDataService = staticDataService;
            Item = carShopItem;

            _price.text = Item.Price.ToString();
            StatsPanel.Refresh(_staticDataService.ForCar(Item.CarTypeId));
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}