using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class TileMap : MonoBehaviour
{

    private int _gridWidth = 5;//Width
    public int gridWidth
    {
        get
        {
            return _gridWidth;
        }
        set
        {
            _gridWidth = value;
            updateCollider();
        }
    }

    private int _gridLength = 5;//length
    public int gridLength
    {
        get
        {
            return _gridLength;
        }
        set
        {
            _gridLength = value;
            updateCollider();
        }
    }
    private float _gridSize = 1;
    public float gridSize
    {
        get
        {
            return _gridSize;
        }
        set
        {
            _gridSize = value;
            updateCollider();
        }
    }

    private BoxCollider _collider;
    void Awake()
    {
        //setting up  collider
        //check if collider already exist
        BoxCollider col = GetComponent<BoxCollider>();
        if (col != null)
        {
            _collider = col;
            return;
        }
        _collider = gameObject.AddComponent<BoxCollider>();
        _collider.isTrigger = true;

    }

    //update collider dimension when field changes
    void updateCollider()
    {
        _collider.center = new Vector3((float)_gridLength / 2, 0, (float)_gridWidth / 2) * gridSize;
        _collider.size = new Vector3(_gridLength, 0, _gridWidth) * gridSize;
    }

    void OnDrawGizmos()
    {


        //Draw a line of width

        for (int i = 0; i <= _gridWidth; i++)

        {

            Gizmos.DrawLine(new Vector3(0, 0, i) + transform.position, new Vector3(_gridLength, 0, i) + transform.position);


        }


        //Draw the length of the line

        for (int i = 0; i <= _gridLength; i++)

        {

            Gizmos.DrawLine(new Vector3(i, 0, 0) + transform.position, new Vector3(i, 0, _gridWidth) + transform.position);

        }




        HandleUtility.Repaint();


    }


}
