using UnityEngine;

namespace CodeBase.Infrastructure.Service
{
    public class StandardInputService : InputService
    {
        public override Vector2 Axis => 
            UnityAxis();
        public override bool IsBreakingButtonPress() =>
            UnityBreakingButtonPress();

        private Vector2 UnityAxis() =>
            new Vector2(Input.GetAxis(HORIZONTAL), Input.GetAxis(VERTICAL));
        private bool UnityBreakingButtonPress() =>
            Input.GetKey(KeyCode.Space);
    }
}
