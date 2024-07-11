using Assets.CodeBase.Services;

namespace CodeBase.Services.Ad
{
    public interface IAdService : IService
    {
        void ShowInterstitial();
        void ShowVideo();
        void StickyAdHide();
        void StickyAdShow();
    }
}