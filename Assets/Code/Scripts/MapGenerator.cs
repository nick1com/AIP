using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    public int numCubes = 50; // Number of cubes to generate
    public float cubeSize = 1f; // Size of each cube
    public float spacing = 0.1f; // Spacing between cubes
    public float turnChance = 0.5f; // Chance of turning at each interval
    public float turnAngle = 90f; // Angle to turn when a turn occurs
    public Color startColor = Color.red; // Color of the first cube
    public Color endColor = Color.green; // Color of the last cube

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        int currentIndex = 0;
        Vector3 currentDirection = Vector3.right;
        GameObject previousCube = null;
        List<Vector3> occupiedPositions = new List<Vector3>(); // List of positions occupied by existing cubes

        while (currentIndex < numCubes)
        {
            // Generate cube and position it relative to the previous cube
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = Vector3.one * cubeSize;
            cube.transform.parent = transform;
            cube.GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, (float)currentIndex / (numCubes - 1));

            if (previousCube == null)
            {
                // Position first cube at origin
                cube.transform.position = Vector3.zero;
                occupiedPositions.Add(cube.transform.position);
            }
            else
            {
                // Position cube based on previous cube and current direction
                Vector3 nextPosition = previousCube.transform.position + currentDirection * (cubeSize + spacing);
                int tries = 0;
                while (occupiedPositions.Contains(nextPosition) && tries < 100)
                {
                    // If the next position is already occupied, randomly rotate the direction and try again
                    currentDirection = GetNewDirection(currentDirection);
                    nextPosition = previousCube.transform.position + currentDirection * (cubeSize + spacing);
                    tries++;
                }

                if (tries >= 100)
                {
                    Debug.LogWarning("Failed to find a valid position for the next cube.");
                    break;
                }

                cube.transform.position = nextPosition;
                occupiedPositions.Add(nextPosition);
            }

            // Randomly turn at certain intervals
            if (Random.value < turnChance && currentIndex < numCubes - 1)
            {
                currentDirection = GetNewDirection(currentDirection);
            }

            // Update variables for next iteration
            previousCube = cube;
            currentIndex++;
        }
    }

    Vector3 GetNewDirection(Vector3 currentDirection)
    {
        // Generate a list of valid directions based on current direction
        List<Vector3> validDirections = new List<Vector3>();
        if (currentDirection != Vector3.left) validDirections.Add(Vector3.right);
        if (currentDirection != Vector3.right) validDirections.Add(Vector3.left);
        validDirections.Add(Vector3.forward);

        // Randomly select a direction from the list
        return validDirections[Random.Range(0, validDirections.Count)];
    }
}

