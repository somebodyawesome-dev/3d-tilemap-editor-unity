using Editor.MonoBehaviour;
using UnityEngine;

namespace Editor.scripts.Controllers
{
    public class TileMapHolderController
    {
        public TileMapHolder initTileMapHolder()
        {
            //look for tilemap holder
            var _tileMapHolder = GameObject.FindObjectOfType<TileMapHolder>();
            //if tilemap holder doesnt exist 
            //create one
            if (_tileMapHolder == null)
            {
                Debug.Log("There is no TileMap Holder");
                Debug.Log("A TileMap Holder wil be created");
                //create tilemap holder
                _tileMapHolder = creatTileMapHolder();
            }

            //get All tileMaps
            _tileMapHolder.getTileMaps();
            return _tileMapHolder;
        }

        public TileMapHolder creatTileMapHolder()
        {
            //create tilemap holder
            var _tileMapHolder = new GameObject("TilesMaps Holder").AddComponent<TileMapHolder>();
            new TileMapController().createNewTileMap(_tileMapHolder);
            return _tileMapHolder;
        }
    }
}