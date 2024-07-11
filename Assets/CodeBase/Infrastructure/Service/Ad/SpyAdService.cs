using System;
using UnityEngine;

namespace CodeBase.Services.Ad
{
    public class SpyAdService : IAdService
    {
        public void ShowInterstitial() => DebugLog(ShowInterstitial);
        public void ShowVideo() => DebugLog(ShowVideo);
        public void StickyAdHide() => DebugLog(StickyAdHide);
        public void StickyAdShow() => DebugLog(StickyAdShow);

        private void DebugLog(Action stickyAdShow) =>
            Debug.Log($"Invoke {this} - {stickyAdShow.Method.Name}");
    }
}