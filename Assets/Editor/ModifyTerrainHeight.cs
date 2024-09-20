using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ModifyTerrainHeight : Editor
{
    [MenuItem("Terrain Tools/Increase Max Height Without Scaling")]
    public static void IncreaseMaxHeightWithoutScaling()
    {
        float sourceHeight = 16;
        float targetHeight = 256;
        Terrain terrain = Selection.activeGameObject.GetComponent<Terrain>();
        Debug.Log(terrain.name);
        TerrainData terrainData = terrain.terrainData;
        float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);

        float[,] newHeights = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int i = 0; i < terrainData.heightmapResolution; i++)
        {
            for (int j = 0; j < terrainData.heightmapResolution; j++)
            {
                newHeights[i, j] = heights[i, j] * (sourceHeight / targetHeight);
                // newHeights[i, j] = 0f;
            }
        }

        terrainData.SetHeights(0, 0, newHeights);
        terrain.Flush();
    }
}
