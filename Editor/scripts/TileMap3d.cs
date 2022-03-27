using Editor.MonoBehaviour;
using Editor.scripts.Controllers;
using Editor.scripts.GUIElements;
using Editor.scripts.MouseStates;
using Editor.scripts.MouseStates.States;
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
        //how many floor there is 

        private int _floors = 1;

        public int floors
        {
            get { return _floors; }
            set { _floors = value; }
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

        // remove mode
        private enum RemoveMode
        {
            BY_INDEX,
            LAST
        }

        private RemoveMode removeMode = RemoveMode.LAST;

        // controller facade
        internal ControllersFacade controllersFacade;

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
            // init controller facade
            controllersFacade = new ControllersFacade();


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
            //styles
            var style = new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter};


            if (GUILayout.Button("Enter/Exit Editor Mode"))
            {
                inEditorMode = !inEditorMode;
                //disable previous brush 
                mouseStateContext.state = new MouseStateDefault(this);
            }


            EditorGUI.BeginDisabledGroup(inEditorMode);
            GUILayout.Label("Grid attributes", EditorStyles.boldLabel);
            GUIField.showField(ref _gridSize, "Grid Size", 1, 100);
            GUIField.showField(ref _gridWidth, "Grid Width", 1, 999);
            GUIField.showField(ref _gridLength, "Grid Length", 1, 999);
            // tilemap index 
            GUIField.showField(ref selectedTileMapIndex, "Tilemap index", 0, controllersFacade.getTileMapsCount() - 1);
            //floor controls buttons
            GUIField.showField(ref _floors, "Floors", 1, 999);

            removeMode = (RemoveMode) EditorGUILayout.EnumPopup("tilemap removing mode", removeMode);
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

            if (GUILayout.Button("delete brush"))
            {
                mouseStateContext.state = new MouseStateRemove(this);
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


        private void addNewTileMap()
        {
            controllersFacade.createNewTileMap(_gridLength, _gridWidth, _gridSize);
        }


        private void removeTileMap()
        {
            if (removeMode == RemoveMode.LAST)
            {
                controllersFacade.removeTileMap();
                return;
            }

            if (removeMode == RemoveMode.BY_INDEX)
            {
                controllersFacade.removeTileMap(selectedTileMapIndex);
            }
        }

        private void onFieldChange()
        {
            //important:  
            //use private field to prevent recursion

            try
            {
                if (selectedTileMapIndex < 0 || selectedTileMapIndex >= controllersFacade.getTileMapsCount())
                {
                    Debug.LogError("please verify the number of floors");
                    Debug.LogError("or enter a valid floor index");

                    return;
                }

                if (_floors <= 0)
                {
                    Debug.LogError("Invalid floor number !");
                    return;
                }

                if (_gridSize <= 0)
                {
                    Debug.LogError("Invalid grid size value !");
                    return;
                }

                if (_gridWidth <= 0)
                {
                    Debug.LogError("Invalid grid width value !");
                    return;
                }

                if (_gridLength <= 0)
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
            //update floor
            updateFloors();
            //tell mouse stat about the change 
            if (mouseStateContext != null)
            {
                mouseStateContext.onFieldsUpdate();
            }
        }

        private void updateFloors()
        {
            var diff = _floors - controllersFacade.getTileMapsCount();

            //add tilemap
            //add at the end
            if (diff > 0)
            {
                addNewTileMap();
                return;
            }

            //remove tilemap
            //remove current or remove the last
            if (diff < 0)
            {
                //TODO:remove tile map
                removeTileMap();
            }
        }

        private void updateTheCurrentTileMap()
        {
            var tilemap = getSelectedTileMap();
            controllersFacade.updateTileMap(tilemap, _gridLength, _gridWidth, _gridSize);
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
            return controllersFacade.getTileMapHolder().tilemaps[selectedTileMapIndex];
        }
    }
}