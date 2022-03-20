﻿using Editor.MonoBehaviour;

namespace Editor.scripts.Controllers
{
    public class ControllersFacade
    {
        private readonly TileMapHolder tileMapHolder;
        private readonly TileMapController tileMapController;
        private TileMapHolderController tileMapHolderController;

        public ControllersFacade()
        {
            this.tileMapController = new TileMapController();
            this.tileMapHolderController = new TileMapHolderController();

            //init TileMap Holder
            this.tileMapHolder = tileMapHolderController.initTileMapHolder();
        }

        /////////////////////// Tile Map Holder Interface ///////////////////
        public TileMapHolder getTileMapHolder()
        {
            return tileMapHolder;
        }

        public int getTileMapsCount()
        {
            return tileMapHolder.tilemaps.Count;
        }

        /////////////////////// Tile Map Interface //////////////////////////
        public void createNewTileMap(int length = 5, int width = 5, float size = 1)
        {
            tileMapController.createNewTileMap(tileMapHolder, length, width, size);
        }

        public void updateTileMap(TileMap tilemap, int length, int width, float size)
        {
            tileMapController.updateTileMap(tilemap, length, width, size);
        }

        public void removeTileMap()
        {
            tileMapController.removeTileMap(tileMapHolder);
        }

        public void removeTileMap(int index)
        {
            tileMapController.removeTileMap(tileMapHolder, index);
        }

        public void removeTileMap(TileMap tilemap)
        {
            tileMapController.removeTileMap(tileMapHolder, tilemap);
        }
    }
}