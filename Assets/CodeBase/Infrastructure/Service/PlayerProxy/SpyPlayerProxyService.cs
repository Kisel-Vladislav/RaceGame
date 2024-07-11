using UnityEngine;

namespace CodeBase.Infrastructure.Service.PlayerProxy
{
    public class SpyPlayerProxyService : IPlayerProxyService
    {
        public bool IsAuthorized => false;

        public void Authorize() =>
            Debug.Log("Try Authorize");
    }
}