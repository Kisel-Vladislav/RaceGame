using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.UI
{
    public class LoadLevelButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private LevelStaticData Level;

        public void OnPointerClick(PointerEventData eventData) =>
            Game.Instance.StateMachine.Enter<LoadLevelState, LevelStaticData>(Level);
    }
}