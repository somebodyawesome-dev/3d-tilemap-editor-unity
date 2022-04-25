using Editor.MonoBehaviour;
using UnityEngine;

namespace Editor.scripts.Controllers
{
    public class NodeController
    {
        public GameObject createNode(TileMap tileMap, GameObject selectedTile, Vector3 position, Quaternion rotation)
        {
            //spawn cell and attach it to its tilemap

            var emptyObj = new GameObject("Node");

            var node = emptyObj.AddComponent<Node>();
            node.tileMap = tileMap;
            emptyObj.transform.parent = tileMap.transform;
            var obj = GameObject.Instantiate(selectedTile, position, rotation);


            //////////////////
            float newSize = tileMap.gridSize;
            var size = obj.GetComponent<Renderer>().bounds.size;
            //var size = Vector3.one * newSize;
            var rescale = obj.transform.localScale;

            rescale.x = newSize * rescale.x / size.x;
            rescale.y = newSize * rescale.y / size.y;
            rescale.z = newSize * rescale.z / size.z;
            obj.transform.localScale = rescale;

            var offset = obj.GetComponent<Renderer>().bounds.center.y;
            obj.transform.position = new Vector3(emptyObj.transform.position.x,
                (emptyObj.transform.position.y - offset),
                emptyObj.transform.position.z);
            obj.transform.parent = emptyObj.transform;
            node.child = obj;


            return emptyObj;
        }

        public GameObject copyNode(TileMap tileMap, GameObject node, Vector3 position, Quaternion rotation)
        {
            var obj = GameObject.Instantiate(node.gameObject, position, rotation);
            obj.transform.parent = tileMap.transform;
            obj.GetComponent<Node>().tileMap = tileMap;
            obj.layer = LayerMask.NameToLayer("Default");

            return obj;
        }

        public void destroyNode(Node node)
        {
            if (node == null) return;
            GameObject.DestroyImmediate(node.gameObject);
        }
    }
}