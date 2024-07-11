using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.Service.Wallet;
using CodeBase.Progress;
using CodeBase.Services.Ad;
using CodeBase.StaticData;
using Scripts.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadMainMenuState : IState
    {
        private const string SceneMainMenuName = "MainMenu";

        private readonly ISaveLoadService _saveLoadService;
        private readonly ICurtain _curtain;
        private readonly IStaticDataService _staticData;
        private readonly IAdService _addService;
        private readonly IWallet _wallet;
        private readonly SceneLoader _sceneLoader;

        public LoadMainMenuState(ISaveLoadService saveLoadService, IStaticDataService staticData, IAdService adService, IWallet wallet, SceneLoader sceneLoader, ICurtain curtain)
        {
            _saveLoadService = saveLoadService;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _staticData = staticData;
            _addService = adService;
            _wallet = wallet;
        }

        public void Enter()
        {
            _curtain.Show();
            _sceneLoader.Load(SceneMainMenuName, OnLoad);
            _curtain.Hide();
        }
        public void Exit()
        {
            ShowAd();

            _saveLoadService.InformProgressWriters();
            _saveLoadService.CleanupCode();
        }
        public void OnLoad()
        {
            ShowAd();
            InitMainMenu();
            RegisterProgressReader(_wallet);
            _saveLoadService.InformProgressReaders();
        }

        private void InitMainMenu()
        {
            var mainMenu = Object.FindFirstObjectByType<MainMenu>();
            InitShop(mainMenu);

            RegisterProgressReader(mainMenu.Shop, _wallet);
        }
        private void ShowAd()
        {
            _addService.ShowInterstitial();
        }
        private void InitShop(MainMenu mainMenu) =>
            mainMenu.Shop.Construct(_wallet, _staticData);
        private void RegisterProgressReader(params ISavedProgress[] savedProgresses)
        {
            foreach (var item in savedProgresses)
                _saveLoadService.Register(item);
        }


    }

}
