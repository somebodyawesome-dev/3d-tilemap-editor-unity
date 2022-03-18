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

        public void addTilemap(TileMap tileMap)
        {
            if (tileMap == null || tilemaps.Contains(tileMap)) return;
            //give tilemap its holder
            tileMap.tileMapHolder = this;
            //set tile map holder as parent
            tileMap.transform.parent = transform;
            tilemaps.Add(tileMap);
            reallocateTilemap();
        }

        private void reallocateTilemap()
        {
            var tilemap = tilemaps[tilemaps.Count - 1];
            var pre_tilemap = tilemaps[tilemaps.Count - 2];
            tilemap.transform.position = pre_tilemap.transform.position + Vector3.up * tilemap.gridSize;
        }
    }
}