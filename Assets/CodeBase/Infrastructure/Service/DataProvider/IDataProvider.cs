using Assets.CodeBase.Services;

namespace CodeBase.Infrastructure.Service
{
    public interface IDataProvider : IService
    {
        public void LoadOrInitNew();
        public void Save();
    }
}