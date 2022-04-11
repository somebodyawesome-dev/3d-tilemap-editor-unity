using Editor.MonoBehaviour;
using UnityEngine;

namespace Editor.scripts.Controllers
{
    public class ControllersFacade
    {
        private readonly TileMapHolder tileMapHolder;
        private readonly TileMapController tileMapController;
        private readonly TileMapHolderController tileMapHolderController;
        private readonly NodeController nodeController;

        public ControllersFacade()
        {
            this.tileMapController = new TileMapController();
            this.tileMapHolderController = new TileMapHolderController();
            this.nodeController = new NodeController();

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

        public void hideTileMapsByIndex(int index = 0)
        {
            tileMapHolderController.hideTileMapsByIndex(tileMapHolder, index);
        }

        public void showAllTileMaps()
        {
            tileMapHolderController.showAllTileMaps(tileMapHolder);
        }

        /////////////////////// Tile Map Interface //////////////////////////
        public void createNewTileMap(int length = 5, int width = 5, float size = 1)
        {
            tileMapController.createNewTileMap(tileMapHolder, length, width, size);
        }

        public void updateTileMap(TileMap tilemap, int length, int width, float size)
        {
            tileMapController.updateTileMap(tilemap, length, width, size);
            //re position all tilemaps
            tileMapHolder.reallocateTilemaps();
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


        public void updateFieldFromTileMap(TileMap tilemap, ref int length, ref int width, ref float size)
        {
            length = tilemap.gridLength;
            size = tilemap.gridSize;
            width = tilemap.gridWidth;
        }
        //////////////////////// Node Interface /////////////////////////

        public GameObject createNode(TileMap tileMap, GameObject selectedTile, Vector3 position, Quaternion rotation)
        {
            return nodeController.createNode(tileMap, selectedTile, position, rotation);
        }

        public void destroyNode(Node node)
        {
            nodeController.destroyNode(node);
        }
    }
}