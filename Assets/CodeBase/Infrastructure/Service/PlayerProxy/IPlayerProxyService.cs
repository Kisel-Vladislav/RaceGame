using Assets.CodeBase.Services;

namespace CodeBase.Infrastructure.Service.PlayerProxy
{
    public interface IPlayerProxyService : IService
    {
        bool IsAuthorized { get; }

        void Authorize();
    }
}