using UnityEngine;

namespace Editor.scripts.MouseStates
{
    public class MouseStateContext
    {
        MouseState _state;
        TileMap3d tileMap3D;

        public MouseStateContext(MouseState _state, TileMap3d tileMap3D)
        {
            this._state = _state;
            this.tileMap3D = tileMap3D;
        }

        public MouseState state
        {
            get { return _state; }
            set
            {
                _state.onDestroy();
                _state = value;
                Debug.Log("Changing Mouse Stat to: " + _state.GetType().Name);
            }
        }

        public void onMouseClick()
        {
            _state.onMouseClick();
        }

        public void OnDrawGizmos()
        {
            _state.OnDrawGizmos();
        }

        public void onDestroy()
        {
            _state.onDestroy();
        }

        public void onFieldsUpdate()
        {
            _state.onFieldsUpdate();
        }
    }
}