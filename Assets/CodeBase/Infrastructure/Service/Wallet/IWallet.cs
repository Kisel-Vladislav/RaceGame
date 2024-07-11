using Assets.CodeBase.Services;
using CodeBase.Progress;
using System;

namespace CodeBase.Infrastructure.Service.Wallet
{
    public interface IWallet : IService, ISavedProgressReader
    {
        int Money { get; }

        event Action MoneyChanged;

        bool IsEnough(int price);
        bool TrySpend(int price);
        void Spend(int price);
        void AddMoney(int coins);

    }
}