using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGenerator : MonoBehaviour
{
    public ForestGenerationData forestGenerationData;
    private List<Vector2Int> forestRooms;

    private void Start()
    {
        forestRooms = ForestCrawlerController.GenerateForest(forestGenerationData);
        SpawnRooms(forestRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0,0);
        foreach(Vector2Int roomLocation in rooms)
        {
            RoomController.instance.LoadRoom("Default", roomLocation.x, roomLocation.y);
        }
    }
}
