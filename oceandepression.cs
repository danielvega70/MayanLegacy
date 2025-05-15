using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oceandepression : MonoBehaviour
{
    public Terrain mainTerrain; // El terrain principal
    public Terrain surroundingTerrain; // El terrain secundario alrededor

    public float oceanLevel = -10f; // Nivel del océano (ajustado en negativo)
    public float depressionDepth = -15f; // Profundidad de la depresión para crear el relieve hacia abajo
    public float areaWidth = 100f; // Ancho de la zona de depresión
    public float areaLength = 100f; // Longitud de la zona de depresión
    public float smoothness = 0.1f; // Qué tan suave será la transición entre el terreno principal y la depresión

    void Start()
    {
        CreateOceanDepressionArea();
    }

    void CreateOceanDepressionArea()
    {
        // Obtener las posiciones de las alturas en el terrain principal
        TerrainData terrainData = surroundingTerrain.terrainData;

        // Crear una matriz para las alturas del terrain
        float[,] heights = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        // Establecer las alturas alrededor del terreno principal para crear una depresión suave
        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int y = 0; y < terrainData.heightmapResolution; y++)
            {
                // Obtener las coordenadas del terreno
                float worldX = x / (float)terrainData.heightmapResolution * terrainData.size.x;
                float worldY = y / (float)terrainData.heightmapResolution * terrainData.size.z;

                // Verificar si estamos dentro del área de la depresión
                if (worldX >= mainTerrain.transform.position.x - areaWidth / 2 &&
                    worldX <= mainTerrain.transform.position.x + areaWidth / 2 &&
                    worldY >= mainTerrain.transform.position.z - areaLength / 2 &&
                    worldY <= mainTerrain.transform.position.z + areaLength / 2)
                {
                    // Ajustar la altura para crear una depresión hacia abajo
                    float distanceToCenterX = Mathf.Abs(worldX - mainTerrain.transform.position.x);
                    float distanceToCenterY = Mathf.Abs(worldY - mainTerrain.transform.position.z);
                    float distanceToCenter = Mathf.Sqrt(distanceToCenterX * distanceToCenterX + distanceToCenterY * distanceToCenterY);

                    // Usar la distancia para suavizar la transición
                    float heightAdjustment = Mathf.Lerp(depressionDepth, oceanLevel, distanceToCenter / (areaWidth / 2));

                    heights[x, y] = heightAdjustment;
                }
                else
                {
                    // Mantener el nivel del terreno original (o el valor del océano si es fuera del área de depresión)
                    heights[x, y] = oceanLevel;
                }
            }
        }

        // Aplicar los cambios de altura en el terreno secundario
        surroundingTerrain.terrainData.SetHeights(0, 0, heights);
    }
}