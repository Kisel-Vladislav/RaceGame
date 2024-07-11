using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] protected Button _back;

        public event Action Opened;
        public event Action Closed;
        protected void ShopEnable() => _back.onClick.AddListener(Hide);
        protected void ShopDisable() => _back.onClick.RemoveListener(Hide);

        public void Hide()
        {
            gameObject.SetActive(false);
            Closed?.Invoke();
        }
        public void Show()
        {
            gameObject.SetActive(true);
            Opened?.Invoke();
        }
    }
}