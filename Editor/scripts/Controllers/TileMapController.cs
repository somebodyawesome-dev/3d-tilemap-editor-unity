using System;
using Editor.MonoBehaviour;
using UnityEngine;

namespace Editor.scripts.Controllers
{
    public class TileMapController
    {
        public void createNewTileMap(TileMapHolder tileMapHolder, int length = 5, int width = 5, float size = 1)
        {
            var newTileMap = new GameObject("tilemap " + tileMapHolder.tilemaps.Count.ToString());
            var tilemap = newTileMap.AddComponent<TileMap>();

            //set up tile map parameters
            updateTileMap(tilemap, length, width, size);
            tilemap.tileMapHolder = tileMapHolder;

            //add tilemap to list of tile maps
            tileMapHolder.addTilemap(tilemap);
        }

        public void updateTileMap(TileMap tilemap, int length, int width, float size)
        {
            if (tilemap == null)
            {
                throw new Exception("tilemap reference is null");
            }

            tilemap.gridSize = size;
            tilemap.gridLength = length;
            tilemap.gridWidth = width;
        }


        public void removeTileMap(TileMapHolder tileMapHolder)
        {
            var tilemaps = tileMapHolder.tilemaps;
            var tilemap = tilemaps[tilemaps.Count - 1];
            //remove tilemap
            removeTileMap(tileMapHolder, tilemap);
        }

        public void removeTileMap(TileMapHolder tileMapHolder, int index)
        {
            var tilemaps = tileMapHolder.tilemaps;
            removeTileMap(tileMapHolder, tilemaps[index]);
        }

        public void removeTileMap(TileMapHolder tileMapHolder, TileMap tilemap)
        {
            if (tilemap == null) return;
            tileMapHolder.tilemaps.Remove(tilemap);
            GameObject.DestroyImmediate(tilemap.gameObject);
        }
    }
}