using System.Collections.Generic;
using UnityEngine;

namespace Editor.MonoBehaviour
{
    [ExecuteInEditMode]
    public class TileMapHolder : UnityEngine.MonoBehaviour
    {
        public List<TileMap> tilemaps = new List<TileMap>();


        public void getTileMaps()
        {
            tilemaps.Clear();
            //get all existing tilemaps
            var arr = GetComponentsInChildren<TileMap>();
            foreach (var item in arr)
            {
                tilemaps.Add(item);
            }
        }

    }
}