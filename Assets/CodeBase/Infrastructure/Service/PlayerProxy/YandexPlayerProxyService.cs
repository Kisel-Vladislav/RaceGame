using Agava.YandexGames;

namespace CodeBase.Infrastructure.Service.PlayerProxy
{
    public class YandexPlayerProxyService : IPlayerProxyService
    {
        public bool IsAuthorized => PlayerAccount.IsAuthorized;

        public void Authorize() => PlayerAccount.Authorize();
    }
}