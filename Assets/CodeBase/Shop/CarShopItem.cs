using CodeBase.Car;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Shop
{
    [CreateAssetMenu(fileName = "CarShopItem", menuName = "Shop/CarShopItem")]
    public class CarShopItem : ShopItem
    {
        public CarTypeId CarTypeId;
    }
}