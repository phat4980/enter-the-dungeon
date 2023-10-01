using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Floor : MonoBehaviour
{
    public GridLayout grid;
    public Tilemap tilemap;
    public int totalRoomInChamber;
    public int i;
    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponentInParent<GridLayout>();
        tilemap = GetComponent<Tilemap>();
    }
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ( i < totalRoomInChamber)
        {
            if (collision.tag == "Room")
            {
                // Same as OnTriggerEnter2D()
                var cellBounds = new BoundsInt(
                    grid.WorldToCell(collision.bounds.min),
                    grid.WorldToCell(collision.bounds.size) + new Vector3Int(0, 0, 1));

                IdentifyIntersections(collision, cellBounds);
            }
        }
    }
    void IdentifyIntersections(Collider2D other, BoundsInt cellBounds)
    {
        RoomManager m = other.GetComponent<RoomManager>();
        if (m != null)
        {
            foreach (var cell in cellBounds.allPositionsWithin)
            {
                // First check if there's a tile in this cell
                if (tilemap.HasTile(cell))
                {
                    // Find closest world point to this cell's center within other collider
                    var cellWorldCenter = grid.CellToWorld(cell);
                    var otherClosestPoint = other.ClosestPoint(cellWorldCenter);
                    var otherClosestCell = grid.WorldToCell(otherClosestPoint);

                    // Check if intersection point is within this cell
                    if (otherClosestCell == cell)
                    {
                        if (!m.trackedCells.Contains(cell))
                        {
                            // other collider just entered this cell
                            m.trackedCells.Add(cell);
                        }
                    }
                }
            }
        }
        other.gameObject.SetActive(false);
        i += 1;
    }
}
