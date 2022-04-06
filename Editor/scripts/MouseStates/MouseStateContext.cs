using UnityEngine;

namespace Editor.scripts.MouseStates
{
    public class MouseStateContext
    {
        MouseState _state;


        public MouseStateContext(MouseState _state)
        {
            this._state = _state;
        }

        public MouseState state
        {
            get { return _state; }
            set
            {
                //if there is mouse state call on destroy
                _state?.onDestroy();
                _state = value;
                Debug.Log("Changing Mouse Stat to: " + _state.GetType().Name);
            }
        }

        public void onMouseClick()
        {
            _state?.onMouseClick();
        }

        public void OnDrawGizmos()
        {
            _state?.OnDrawGizmos();
        }

        public void onDestroy()
        {
            _state?.onDestroy();
        }

        public void onFieldsUpdate()
        {
            _state?.onFieldsUpdate();
        }
    }
}