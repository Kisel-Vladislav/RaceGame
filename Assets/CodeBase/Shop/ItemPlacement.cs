using Scripts;
using UnityEngine;

namespace CodeBase.Shop
{
    public class ItemPlacement : MonoBehaviour
    {
        private const string RenderLayer = "SkinRender";

        [SerializeField] private Rotator _rotator;
        private GameObject _currentModel;

        public void InstantiateModel(GameObject model)
        {
            if (_currentModel != null)
                Destroy(_currentModel.gameObject);

            _rotator.ResetRotation();

            _currentModel = Instantiate(model, transform);

            var childrens = _currentModel.GetComponentsInChildren<Transform>();

            foreach (var item in childrens)
                item.gameObject.layer = LayerMask.NameToLayer(RenderLayer);
        }
    }
}
