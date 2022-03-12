using Editor.MonoBehaviour;
using Editor.scripts.MouseStates;
using UnityEditor;
using UnityEngine;

namespace Editor.scripts
{
    public static class GizmoDrawer
    {
        public static MouseStateContext mouseStateContext = null;

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        private static void DrawGizmoForTileMapHolder(TileMapHolder tileMapHolder, GizmoType gizmoType)
        {
            // if there is mouse state context instance (editor window is open)
            //subscribe to draw gizmo 
            if (mouseStateContext == null)
            {
                return;
            }

            mouseStateContext.OnDrawGizmos();
        }

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        private static void DrawGridGizmo(TileMap tileMap, GizmoType gizmoType)
        {
            //Draw a line of width

            for (var i = 0; i <= tileMap._gridWidth; i++)

            {
                var position = tileMap.transform.position;
                var startingPoint = new Vector3(0, 0, i) + position;
                var endingPoint = new Vector3(tileMap._gridLength, 0, i) + position;
                Gizmos.DrawLine(startingPoint, endingPoint);
            }


            //Draw the length of the line

            for (var i = 0; i <= tileMap._gridLength; i++)

            {
                var position = tileMap.transform.position;
                var startingPoint = new Vector3(i, 0, 0) + position;
                var endingPoint = new Vector3(i, 0, tileMap._gridWidth) + position;
                Gizmos.DrawLine(startingPoint, endingPoint);
            }


            HandleUtility.Repaint();
        }
    }
}