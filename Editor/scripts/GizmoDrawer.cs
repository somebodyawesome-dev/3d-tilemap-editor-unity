using Editor.mono;
using UnityEditor;

namespace Editor.scripts
{
    public static class GizmoDrawer
    {
        public static MouseStateContext mouseStateContext = null;

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        static void DrawGizmoForTileMapHolder(TileMapHolder tileMapHolder, GizmoType gizmoType)
        {
            // if there is mouse state context instance (editor window is open)
            //subscirbe to drawgizmo 
            if (mouseStateContext != null)
            {

                mouseStateContext.OnDrawGizmos();
            }
        }

    }
}
