using Editor.MonoBehaviour;
using UnityEngine;

namespace Editor.scripts.Controllers
{
    public class NodeController
    {
        public GameObject createNode(TileMap tileMap, GameObject selectedTile, Vector3 position, Quaternion rotation)
        {
            //spawn cell and attach it to its tilemap

            var obj = GameObject.Instantiate(selectedTile, position, rotation).AddComponent<Node>();
            obj.tileMap = tileMap;
            obj.transform.parent = tileMap.transform;
            return obj.gameObject;
        }

        public void destroyNode(Node node)
        {
            GameObject.DestroyImmediate(node.gameObject);
        }
    }
}