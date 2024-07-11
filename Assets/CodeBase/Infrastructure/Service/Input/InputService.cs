using UnityEngine;

namespace CodeBase.Infrastructure.Service
{
    public abstract class InputService : IInputService
    {
        protected const string HORIZONTAL = "Horizontal";
        protected const string VERTICAL = "Vertical";

        public abstract Vector2 Axis { get; }
        public abstract bool IsBreakingButtonPress();
    }
}