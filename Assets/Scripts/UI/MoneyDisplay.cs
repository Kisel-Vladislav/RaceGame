using CodeBase.Infrastructure.Service.Wallet;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class MoneyDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private IWallet _wallet;

        public void Construct(IWallet wallet) => 
            _wallet = wallet;

        private void OnEnable()
        {
            Refresh();
            _wallet.MoneyChanged += Refresh;
        }
        private void OnDisable() => 
            _wallet.MoneyChanged -= Refresh;

        private void Refresh() => 
            text.text = _wallet.Money.ToString();

    }
}