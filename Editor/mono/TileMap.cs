using UnityEditor;
using UnityEngine;

namespace Editor.mono
{
    [ExecuteInEditMode]
    public class TileMap : MonoBehaviour
    {
        private int _gridWidth = 5; //Width

        public int gridWidth
        {
            get { return _gridWidth; }
            set
            {
                _gridWidth = value;
                updateCollider();
            }
        }

        private int _gridLength = 5; //length

        public int gridLength
        {
            get { return _gridLength; }
            set
            {
                _gridLength = value;
                updateCollider();
            }
        }

        private float _gridSize = 1;

        public float gridSize
        {
            get { return _gridSize; }
            set
            {
                _gridSize = value;
                updateCollider();
            }
        }

        private BoxCollider _collider;

        void Awake()
        {
            //setting up  collider
            //check if collider already exist
            BoxCollider col = GetComponent<BoxCollider>();
            if (col != null)
            {
                _collider = col;
                return;
            }

            _collider = gameObject.AddComponent<BoxCollider>();
            _collider.isTrigger = true;
        }

        //update collider dimension when field changes
        private void updateCollider()
        {
            _collider.center = new Vector3((float) _gridLength / 2, 0, (float) _gridWidth / 2) * gridSize;
            _collider.size = new Vector3(_gridLength, 0, _gridWidth) * gridSize;
        }

        private void OnDrawGizmos()
        {
            //Draw a line of width

            for (var i = 0; i <= _gridWidth; i++)

            {
                var position = transform.position;
                var startingPoint = new Vector3(0, 0, i) + position;
                var endingPoint = new Vector3(_gridLength, 0, i) + position;
                Gizmos.DrawLine(startingPoint, endingPoint);
            }


            //Draw the length of the line

            for (var i = 0; i <= _gridLength; i++)

            {
                var position = transform.position;
                var startingPoint = new Vector3(i, 0, 0) + position;
                var endingPoint = new Vector3(i, 0, _gridWidth) + position;
                Gizmos.DrawLine(startingPoint, endingPoint);
            }


            HandleUtility.Repaint();
        }
    }
}