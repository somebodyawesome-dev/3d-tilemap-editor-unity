using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Editor.MonoBehaviour
{
    [ExecuteInEditMode]
    public class TileMap : UnityEngine.MonoBehaviour
    {
        private TileMapHolder _tileMapHolder;

        public TileMapHolder tileMapHolder
        {
            get { return _tileMapHolder; }
            set
            {
                //remove from previous tilemap holder
                if (_tileMapHolder != null)
                {
                    _tileMapHolder.removeTileMap(this);
                }

                _tileMapHolder = value;
                //update the new tilemap holder
                if (_tileMapHolder != null)
                {
                    _tileMapHolder.addTilemap(this);
                }
            }
        }

        internal int _gridWidth = 5; //Width

        public int gridWidth
        {
            get { return _gridWidth; }
            set
            {
                _gridWidth = value;
                updateCollider();
            }
        }

        internal int _gridLength = 5; //length

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
                resizeNodes();
            }
        }

        private BoxCollider _collider;
        public List<Node> nodes = new List<Node>();

        private void Awake()
        {
            //setting up  collider
            //check if collider already exist
            var col = GetComponent<BoxCollider>();
            if (col != null)
            {
                _collider = col;
                return;
            }

            _collider = gameObject.AddComponent<BoxCollider>();
            _collider.center = new Vector3((float) 1 / 2, 0, (float) 1 / 2);
            _collider.size = new Vector3(1, 0, 1) * gridSize;
            _collider.isTrigger = true;
        }

        //update collider dimension when field changes
        private void updateCollider()
        {
            transform.localScale = new Vector3(_gridLength, 1, gridWidth) * gridSize;
        }

        private void resizeNodes()
        {
            foreach (var node in nodes)
            {
            }
        }

        public void addNode(Node node)
        {
            if (node == null || nodes.Contains(node)) return;
            nodes.Add(node);
        }

        public bool removeNode(Node node)
        {
            return nodes.Remove(node);
        }

        private void OnDestroy()
        {
            tileMapHolder.getTileMaps();
        }
    }
}