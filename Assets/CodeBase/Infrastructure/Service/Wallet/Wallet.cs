using CodeBase.Progress;
using System;

namespace CodeBase.Infrastructure.Service.Wallet
{
    public class Wallet : ISavedProgressReader, IWallet
    {
        private int _money;

        public int Money { get => _money; private set => _money = value; }

        public event Action MoneyChanged;

        public bool IsEnough(int price) => Money - price >= 0;
        public void AddMoney(int coins)
        {
            Money += coins;

            MoneyChanged?.Invoke();
        }
        public bool TrySpend(int price)
        {
            if (IsEnough(price))
            {
                Spend(price);
                return true;
            }
            return false;
        }
        public void Spend(int price)
        {
            Money -= price;
            MoneyChanged?.Invoke();
        }
        public void LoadProgress(PlayerProgress progress)
        {
            Money = progress.Money;
            MoneyChanged?.Invoke();
        }
        public void SaveProgress(PlayerProgress progress)
        {
            progress.Money = Money;
        }
    }
}