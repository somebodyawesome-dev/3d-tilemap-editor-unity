using System;
using Editor.MonoBehaviour;
using UnityEngine;

namespace Editor.scripts
{
    public static class TileMapController
    {
        public static void addNewTileMap(int length, int width, float size)
        {
            var newTileMap = new GameObject("tilemap");
            var tilemap = newTileMap.AddComponent<TileMap>();

            //set up tile map parameters
            updateTileMap(tilemap, length, width, size);


            var tileMapHolder = GameObject.FindObjectOfType<TileMapHolder>();
            if (tileMapHolder == null) Debug.Log("TilemapHolder cant be found ");
            //set tile map holder as parent  
            newTileMap.transform.parent = tileMapHolder.transform;
            //add tilemap to list of tile maps
            tileMapHolder.tilemaps.Add(tilemap);
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
    }
}