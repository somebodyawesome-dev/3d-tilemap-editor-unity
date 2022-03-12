using System;
using Editor.MonoBehaviour;
using UnityEngine;

namespace Editor.scripts
{
    public static class TileMapController
    {
        public static void createNewTileMap(int length, int width, float size)
        {
            var tileMapHolder = GameObject.FindObjectOfType<TileMapHolder>();
            if (tileMapHolder == null) Debug.Log("TilemapHolder cant be found ");


            var newTileMap = new GameObject("tilemap " + tileMapHolder.tilemaps.Count.ToString());
            var tilemap = newTileMap.AddComponent<TileMap>();

            //set up tile map parameters
            updateTileMap(tilemap, length, width, size);


            //add tilemap to list of tile maps
            tileMapHolder.addTilemap(tilemap);
        }

        public static void updateTileMap(TileMap tilemap, int length, int width, float size)
        {
            if (tilemap == null)
            {
                throw new Exception("tilemap reference is null");
            }

            tilemap.gridSize = size;
            tilemap.gridLength = length;
            tilemap.gridWidth = width;
        }

        public static void removeTileMap()
        {
        }
    }
}