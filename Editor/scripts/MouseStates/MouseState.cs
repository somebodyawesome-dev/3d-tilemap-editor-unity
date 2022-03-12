using Editor.MonoBehaviour;
using UnityEditor;
using UnityEngine;

namespace Editor.scripts
{
    public abstract class MouseState
    {
        protected readonly TileMap3d tileMap3D;

        protected MouseState(TileMap3d tile)
        {
            tileMap3D = tile;
        }

        public abstract void onMouseClick();
        public abstract void OnDrawGizmos();
        public abstract void onFieldsUpdate();
        public abstract void onDestroy();
    }
}