
using UnityEngine;
using UnityEditor;

public abstract class MouseState
{
    public TileMap3d tileMap3D;
    protected MouseState(TileMap3d tile)
    {
        tileMap3D = tile;
    }
    public abstract void onMouseClick();
    public abstract void OnDrawGizmos();
    public abstract void onFieldsUpdate();
    public abstract void onDestroy();
}


/// <summary>
///  default brush 
/// </summary>
public class MouseStateDefault : MouseState
{
    public MouseStateDefault(TileMap3d tile) : base(tile)
    {

    }
    public override void onMouseClick()
    {
        Debug.Log("default Click");

    }
    public override void OnDrawGizmos()
    {

    }
    public override void onFieldsUpdate()
    {

    }
    public override void onDestroy()
    {

    }
}

/// <summary>
///  Paint brush
/// </summary>
public class MouseStatePaint : MouseState
{
    private GameObject currentCell = null;
    public MouseStatePaint(TileMap3d tile) : base(tile)
    {
        spawnGhostCell();
    }
    public override void onMouseClick()
    {
        //prevent object selection in scene view
        int controlId = GUIUtility.GetControlID(FocusType.Passive);
        GUIUtility.hotControl = controlId;
        Event.current.Use();



        //if there is not stile selected
        if (currentCell == null)
        {
            Debug.Log("there's no tile selected");
            return;
        }
        //TODO: spawn node  on the clicked cell
        Debug.Log("spawn cube");

        // get postion on  scene view
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        int layerMask = LayerMask.GetMask("Ignore Raycast");
        RaycastHit hit;
        //casting ray to check for colliding on the tilemap
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000, ~layerMask))
        {
            TileMap tilemap = hit.collider.GetComponent<TileMap>();
            //if ray hit tilemap 
            if (tilemap != null)
            {


                //TODO: get and collor the cell correspand to the mousepostion
                //TODO: offset to grid size

                // Vector3 cell = (hit.point - tilemap.transform.position).Truncate(Vector3.one * 0.5f * tilemap.gridSize) + tileMap3D.tileMapHolder.transform.position;
                // init node and spawn it
                spawnNode(tileMap3D.selectedTile, currentCell.transform.position, currentCell.transform.rotation);

            }

        }
        HandleUtility.Repaint();

    }
    public override void OnDrawGizmos()
    {

        if (currentCell == null) return;
        // getting mouse postion on  scene view
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

        RaycastHit hit;
        //casting ray to check for colliding on the tilemap
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000))
        {
            TileMap tilemap = hit.collider.GetComponent<TileMap>();
            //if ray hit tilemap 
            if (tilemap != null)
            {

                //TODO: get and collor the cell correspand to the mousepostion
                //TODO: offset to grid size

                Vector3 cell = (hit.point - tilemap.transform.position).Truncate(Vector3.one * 0.5f * tilemap.gridSize) + tileMap3D.tileMapHolder.gameObject.transform.position;
                //check if still on the same cell 
                //TODO: check node if its occupied
                if (!cell.Equals(currentCell.transform.position))
                {

                    currentCell.transform.position = cell;
                }



            }

        }
        HandleUtility.Repaint();
    }
    public override void onFieldsUpdate()
    {
        GameObject newTile = tileMap3D.selectedTile;



        spawnGhostCell();

    }
    void spawnGhostCell()
    {

        //destroy previous cell if it exist
        if (currentCell != null) { GameObject.DestroyImmediate(currentCell); }
        //check if there is selected tile
        if (tileMap3D.selectedTile == null) { return; }

        //spawn the ghost cell
        currentCell = GameObject.Instantiate(tileMap3D.selectedTile);
        currentCell.name = "Ghost Cell";
        currentCell.transform.parent = tileMap3D.tileMapHolder.transform;
        currentCell.layer = LayerMask.NameToLayer("Ignore Raycast");

    }
    void spawnNode(GameObject selectedTile, Vector3 position, Quaternion rotation)
    {
        //spawn cell and attach it to its tilemap

        var obj = GameObject.Instantiate(selectedTile, position, rotation);
        obj.transform.parent = tileMap3D.getSelectedTileMap().transform;
    }
    public override void onDestroy()
    {
        GameObject.DestroyImmediate(currentCell);
    }
}