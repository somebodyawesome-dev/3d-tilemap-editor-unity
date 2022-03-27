using System;
using UnityEngine;

namespace Editor.MonoBehaviour
{
    [ExecuteInEditMode]
    public class Node : UnityEngine.MonoBehaviour
    {
        private TileMap _tileMap;
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
                }
            }
        }

        private void Start()
        {
            collider = GetComponent<BoxCollider>();
            if (collider == null)
            {
                collider = this.gameObject.AddComponent<BoxCollider>();
            }

            resize();
        }

        public void resize()
        {
            float newSize = _tileMap.gridSize;
            var size = GetComponent<Renderer>().bounds.size;

            var rescale = transform.localScale;

            rescale.x = newSize * rescale.x / size.x;
            rescale.y = newSize * rescale.y / size.y;
            rescale.z = newSize * rescale.z / size.z;
            transform.localScale = rescale;
            //collider.size = GetComponent<Renderer>().bounds.size;
        }
    }
}