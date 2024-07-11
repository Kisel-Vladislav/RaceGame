using Assets.CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Service
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsBreakingButtonPress();
    }
}
