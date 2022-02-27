using Editor.mono;
using UnityEditor;
using UnityEngine;

namespace Editor.scripts
{
    public class TileMap3d : EditorWindow
    {
        //TODO:implement selection of the current tilemap 
        private int selectedTileMapIndex = 0;

        //Grid attribute

        //grid scale
        private float _gridSize = 1;

        public float gridSize
        {
            get { return _gridSize; }
            set { _gridSize = value; }
        }

        // grid width
        private int _gridWidth = 5;

        public int gridWidth
        {
            get { return _gridWidth; }
            set { _gridWidth = value; }
        }

        //grid length
        private int _gridLength = 5;

        public int gridLength
        {
            get { return _gridLength; }
            set { _gridLength = value; }
        }

        //Tile map holder component
        //contains all of the tile maps
        private TileMapHolder _tileMapHolder = null;

        public TileMapHolder tileMapHolder
        {
            get { return _tileMapHolder; }
            set
            {
                _tileMapHolder = value;
                Debug.Log("setting up tile map holder");
            }
        }

        //selected tile 
        //might be prefab or a normal game object
        private GameObject _selectedTile;

        public GameObject selectedTile
        {
            get { return _selectedTile; }
            set
            {
                _selectedTile = value;
                Debug.Log("selected new Game object as Tile");
            }
        }


        // mouse Context  
        // handle mouse presses with state design pattern 
        private MouseStateContext mouseStateContext;


        // editor mode
        private bool inEditorMode = false;

        //open tilemap window editor 
        [MenuItem("Tools/Tile Mapper")]
        public static void showWindow()
        {
            // open tilemap window
            var window = (TileMap3d) GetWindow(typeof(TileMap3d));

            //init  window field 
            window.start();
        }

        private void start()
        {
            //look for tilemap holder
            _tileMapHolder = FindObjectOfType<TileMapHolder>();
            //if tilemap holder doesnt exist 
            //create one
            if (_tileMapHolder == null)
            {
                Debug.Log("There is no TileMap Holder");
                Debug.Log("A TileMap Holder wil be created");
                //create tilemap holder
                initTileMapHolder();
            }

            //get All tileMaps
            _tileMapHolder.getTileMaps();


            //init mouse events
            mouseStateContext = new MouseStateContext(new MouseStateDefault(this), this);


            //init Gizmo Drawer
            GizmoDrawer.mouseStateContext = mouseStateContext;


            //subscribe to SceneView events
            SceneView.duringSceneGui -= OnScene;
            SceneView.duringSceneGui += OnScene;
        }


        //handle mouse events when mouse cursor positioned in the scene view
        private void OnScene(SceneView scene)
        {
            var e = Event.current;

            if (e.type == EventType.MouseDown && e.button == 0)
            {
                mouseStateContext.onMouseClick();
            }
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Editor Mode"))
            {
                inEditorMode = !inEditorMode;
            }

            //TODO: divide fields in groups
            EditorGUI.BeginDisabledGroup(inEditorMode);
            GUILayout.Label("Grid attributes", EditorStyles.boldLabel);
            _gridSize = EditorGUILayout.FloatField("Size of grid", _gridSize);
            _gridWidth = EditorGUILayout.IntField("Grid width", _gridWidth);
            _gridLength = EditorGUILayout.IntField("Grid height", _gridLength);

            selectedTileMapIndex = EditorGUILayout.IntField("selected tile map", selectedTileMapIndex);
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(!inEditorMode);
            GUILayout.Label("Edit Mode", EditorStyles.boldLabel);
            _selectedTile =
                (GameObject) EditorGUILayout.ObjectField("Selected Tile", _selectedTile, typeof(GameObject), false);
            //brushes area
            //TODO: make different types of brushes
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("default brush"))
            {
                mouseStateContext.state = new MouseStateDefault(this);
            }

            if (GUILayout.Button("paint brush"))
            {
                mouseStateContext.state = new MouseStatePaint(this);
            }

            GUILayout.EndHorizontal();
            EditorGUI.EndDisabledGroup();


            if (GUI.changed)
            {
                //one of the fields changed
                onFieldChange();
            }

            GUIUtility.ExitGUI();
            EditorGUILayout.EndToggleGroup();
        }

        private void initTileMapHolder()
        {
            _tileMapHolder = new GameObject("TilesMaps Holder").AddComponent<TileMapHolder>();
            addNewTileMap();
        }

        private void addNewTileMap()
        {
            var newTileMap = new GameObject("tilemap");
            var tilemap = newTileMap.AddComponent<TileMap>();
            //set up tile map parameters
            tilemap.gridSize = _gridSize;
            tilemap.gridLength = _gridLength;
            tilemap.gridWidth = _gridWidth;


            //set tile map holder as parent  
            newTileMap.transform.parent = tileMapHolder.transform;
            //add tilemap to list of tile maps
            tileMapHolder.tilemaps.Add(tilemap);
        }


        private void onFieldChange()
        {
            //important:  
            //use private field to prevent recursion

            try
            {
                if (selectedTileMapIndex < 0 || selectedTileMapIndex >= tileMapHolder.tilemaps.Count)
                {
                    Debug.LogError("please verify the number of floors");
                    Debug.LogError("or enter a valid floor index");

                    return;
                }

                if (_gridSize < 0)
                {
                    Debug.LogError("Invalid grid size value !");
                    return;
                }

                if (_gridWidth < 0)
                {
                    Debug.LogError("Invalid grid width value !");
                    return;
                }

                if (_gridLength < 0)
                {
                    Debug.LogError("Invalid grid Length value !");
                    return;
                }

                // if all fields are valid
                FieldChanged();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.StackTrace);
                //skip due to invalid input 
                Debug.LogError(e.Message);
            }
        }

        private void FieldChanged()
        {
            // update the current tile map
            updateTheCurrentTileMap();
            //tell mouse stat about the change 
            if (mouseStateContext != null)
            {
                mouseStateContext.state.onFieldsUpdate();
            }
        }

        private void updateTheCurrentTileMap()
        {
            var tilemap = tileMapHolder.tilemaps[selectedTileMapIndex];
            tilemap.gridLength = _gridLength;
            tilemap.gridWidth = _gridWidth;
            tilemap.gridSize = _gridSize;
        }

        public void OnDisable()
        {
            Debug.Log("tilemap tool closed !");


            mouseStateContext.onDestroy();
            GizmoDrawer.mouseStateContext = null;

            //unsubscribe to mouse events
            SceneView.duringSceneGui -= OnScene;
        }

        public TileMap getSelectedTileMap()
        {
            return tileMapHolder.tilemaps[selectedTileMapIndex];
        }
    }
}