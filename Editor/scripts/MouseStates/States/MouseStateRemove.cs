using Editor.MonoBehaviour;
using UnityEngine;
using UnityEditor;

namespace Editor.scripts.MouseStates.States
{
    public class MouseStateRemove : MouseState
    {
        public MouseStateRemove(TileMap3d tile) : base(tile)
        {
        }

        public override void onMouseClick()
        {
            //prevent object selection in scene view
            //stop event from propagation
            var controlId = GUIUtility.GetControlID(FocusType.Passive);
            GUIUtility.hotControl = controlId;
            Event.current.Use();

            var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out var hit, 1000))
            {
                var node = hit.collider.GetComponent<Node>();
                //if Node is in selected TileMap Delete
                if (node != null && node.tileMap == tileMap3D.getSelectedTileMap())
                {
                    Debug.Log("deleting");
                    //then remove node
                    tileMap3D.controllersFacade.destroyNode(node);
                }
            }
        }

        public override void OnDrawGizmos()
        {
        }

        public override void onFieldsUpdate()
        {
        }

        public override void onDestroy()
        {
        }
    }
}