using Scripts;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(WayPointContainer))]
    public class WayPointContainerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(WayPointContainer wayPointContainer,GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            foreach (var waypoint in wayPointContainer.Waypoints)
                Gizmos.DrawSphere(waypoint.transform.position, 0.5f);
            Gizmos.color = Color.white;
        }
    }
}