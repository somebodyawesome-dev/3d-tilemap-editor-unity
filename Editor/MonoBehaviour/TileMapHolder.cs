using System.Collections.Generic;
using Editor.scripts;
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

            //set tile map holder as parent
            tileMap.transform.parent = transform;
            tilemaps.Add(tileMap);
            reallocateTilemap();
        }

        public bool removeTileMap(TileMap tileMap)
        {
            return tilemaps.Remove(tileMap);
        }


        private void reallocateTilemap()
        {
            if (tilemaps.Count <= 1) return;
            var tilemap = tilemaps[tilemaps.Count - 1];
            var pre_tilemap = tilemaps[tilemaps.Count - 2];
            tilemap.transform.position = pre_tilemap.transform.position + Vector3.up * pre_tilemap.gridSize;
        }

        public void reallocateTilemaps()
        {
            if (tilemaps.Count <= 1) return;
            for (var i = 1; i < tilemaps.Count; i++)
            {
                var tilemap = tilemaps[i];
                var pre_tilemap = tilemaps[i - 1];
                tilemap.transform.position = pre_tilemap.transform.position + Vector3.up * pre_tilemap.gridSize;
            }
        }
    }
}