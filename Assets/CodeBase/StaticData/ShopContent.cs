using CodeBase.Shop;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "CarShopContent", menuName = "Shop/CarShopContent")]
    public class CarShopContent : ScriptableObject
    {
        public List<CarShopItem> items;
    }
}