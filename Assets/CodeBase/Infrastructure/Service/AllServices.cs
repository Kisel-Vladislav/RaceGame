namespace Assets.CodeBase.Services
{
    public class AllServices
    {
        private static AllServices _instance;

        public static AllServices Container => _instance ??= new AllServices();

        public void RegisterSingle<TService>(TService implementation)
            where TService : IService =>
            ImplementationService<TService>.ServiceInstance = implementation;
        public TService Single<TService>()
            where TService : IService =>
            ImplementationService<TService>.ServiceInstance;

        private static class ImplementationService<TService> 
            where TService : IService 
        {
            public static TService ServiceInstance;
        }
    }
}
