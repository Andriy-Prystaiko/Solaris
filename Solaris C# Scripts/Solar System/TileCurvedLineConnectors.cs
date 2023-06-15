using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCurvedLineConnectors : MonoBehaviour
{
    public Transform Tile1;
    public Transform Tile2;
    public Transform TargetPlanet;
    public LineRenderer CurvedLine;
    public float vertexCount = 12;
    
    // Generate a new point for the curve to follow using the center of the planet and the two tiles
    private Vector3 positionMiddle = new Vector3(0,0,0);
    private Vector3 CurvePoint = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        // Find the Middle point between the two tiles
        positionMiddle = new Vector3((Tile1.transform.position.x + Tile2.transform.position.x) / 2, (Tile1.transform.position.y + Tile2.transform.position.y) / 2, (Tile1.transform.position.z + Tile2.transform.position.z) / 2);
        
        // Find the direction from the center point to the middle of the planet
        Vector3 BetweenToCore = TargetPlanet.position - positionMiddle;

        // Find the direction and distance between the core of the planet and the tile
        Vector3 TileToCore = TargetPlanet.position - Tile1.position;

        //Rotate the Tile to Core vector through the middle point
        Vector3 CoreToSurface = Vector3.RotateTowards(TileToCore, BetweenToCore,2,0);

        // Find the distance between the Tiles
        Vector3 DistanceBetween = Tile1.position - Tile2.position;

        // If statement to modify the Bezier curve based on distance between tiles (Large distances require larger curve to get around planet)
        if (DistanceBetween.magnitude > 0.62)
        {
            // Find the position of the point of which to curve the line around add 2% to make the line above the surface
            CurvePoint = TargetPlanet.position - CoreToSurface * 1.35f;
        }
        else
        {
            // Find the position of the point of which to curve the line around add 2% to make the line above the surface
            CurvePoint = TargetPlanet.position - CoreToSurface * 1.06f;
        }

        // Visualize the new point
        Debug.DrawRay(CurvePoint, CoreToSurface, Color.blue); 

        // Create a list of points along the curve to draw the line through
        var pointList = new List<Vector3>();
        
        // For loop that does through and finds points along the bezier curve between first tile to mid point and mid point to second tile
        for(float ratio = 0; ratio<=1; ratio+= 1/vertexCount)
        {
            var tangent1 = Vector3.Lerp(Tile1.position, CurvePoint, ratio);
            var tangent2 = Vector3.Lerp(CurvePoint, Tile2.position, ratio);
            var curve = Vector3.Lerp(tangent1, tangent2, ratio);

            pointList.Add(curve);
        }

        // Spawn a new line using line prefab
        Instantiate(CurvedLine, new Vector3(0,0,0), Quaternion.identity);
        
        // Give the new line as many points as we indicated in vertex count at coordinates generated in for loop above
        CurvedLine.positionCount = pointList.Count;
        CurvedLine.SetPositions(pointList.ToArray());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

