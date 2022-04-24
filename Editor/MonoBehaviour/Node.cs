using System;
using UnityEngine;

namespace Editor.MonoBehaviour
{
    [ExecuteInEditMode]
    public class Node : UnityEngine.MonoBehaviour
    {
        [SerializeField] private TileMap _tileMap;
        private new BoxCollider collider;

        public TileMap tileMap
        {
            get => _tileMap;
            set
            {
                //remove from previous tilemap
                if (_tileMap != null)
                {
                    _tileMap.removeNode(this);
                }

                _tileMap = value;
                //update new tilemap
                if (_tileMap != null)
                {
                    _tileMap.addNode(this);
                    updateCoordination();
                }
            }
        }

        [SerializeField] internal float x;
        [SerializeField] internal float z;
        public GameObject child;

        public void updateCoordination()
        {
            //get grid cell indexes to re position in case of tilemap size update
            var pos = (transform.position - tileMap.transform.position).Truncate(Vector3.zero);
            x = (pos.x - (pos.x % tileMap.gridSize)) / _tileMap.gridSize;
            z = (pos.z - (pos.z % tileMap.gridSize)) / _tileMap.gridSize;
        }

        public void reposition()
        {
            if (_tileMap == null) return;
            transform.position = new Vector3(x, 0, z) * _tileMap.gridSize +
                                 Vector3.one * 0.5f * _tileMap.gridSize +
                                 Vector3.up * tileMap.transform.position.y;
        }

        private void Start()
        {
            collider = GetComponent<BoxCollider>();
            if (collider == null)
            {
                collider = this.gameObject.AddComponent<BoxCollider>();
                collider.isTrigger = true;
            }


            resize();
        }

        public void resize()
        {
            float newSize = _tileMap.gridSize;
            var size = child.GetComponent<Renderer>().bounds.size;
            //var size = Vector3.one * newSize;
            var rescale = transform.localScale;

            rescale.x = newSize * rescale.x / size.x;
            rescale.y = newSize * rescale.y / size.y;
            rescale.z = newSize * rescale.z / size.z;
            transform.localScale = rescale;


            //collider.size = GetComponent<Renderer>().bounds.size;
        }

        private void OnDestroy()
        {
            //remove from previous tilemap
            if (_tileMap != null)
            {
                _tileMap.removeNode(this);
            }
        }
    }
}