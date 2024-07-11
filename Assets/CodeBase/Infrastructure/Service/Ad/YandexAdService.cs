using Agava.YandexGames;

namespace CodeBase.Services.Ad
{
    public class YandexAdService : IAdService
    {
        public void StickyAdShow() => StickyAd.Show();
        public void StickyAdHide() => StickyAd.Show();
        public void ShowInterstitial() => InterstitialAd.Show();
        public void ShowVideo() => VideoAd.Show();
    }
}