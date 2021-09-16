using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    up = 0,
    left = 1,
    down = 2,
    right = 3
};

public class ForestCrawlerController : MonoBehaviour
{
    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();
    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        {Direction.up, Vector2Int.up},
        {Direction.left, Vector2Int.left},
        {Direction.down, Vector2Int.down},
        {Direction.right, Vector2Int.right}
    };

    public static List<Vector2Int> GenerateForest(ForestGenerationData forestData)
    {
        List<ForestCrawler> forestCrawlers = new List<ForestCrawler>();

        for(int i = 0; i < forestData.numberOfCrawlers; i++)
        {
            forestCrawlers.Add(new ForestCrawler(Vector2Int.zero));
        }

        int iterations = Random.Range(forestData.iterationMin, forestData.iterationMax);

        for(int i = 0; i < iterations; i++)
        {
            foreach(ForestCrawler forestCrawler in forestCrawlers)
            {
                Vector2Int newPos = forestCrawler.Move(directionMovementMap);
                positionsVisited.Add(newPos);
            }
        }

        return positionsVisited;
    }
}
