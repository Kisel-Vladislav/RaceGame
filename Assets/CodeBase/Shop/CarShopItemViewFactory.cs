using Assets.CodeBase.Services;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Shop
{
    public class CarShopItemViewFactory
    {
        private IAssetProvider _assetProvider;
        private IStaticDataService _staticDataService;
        public CarShopItemViewFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }
        public CarShopItemView Create(Transform parent, CarShopItem carShopItem)
        {
            var view = _assetProvider.Instance<CarShopItemView>(AssetsPath.CarShopItem, parent);
            view.Construct(_staticDataService, carShopItem);
            return view;
        }
    }
}