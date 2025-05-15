using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcgenerator : MonoBehaviour
{
    public GameObject npcPrefab;  // Prefab del NPC que se usará para generar nuevos NPCs
    public Transform[] posicionesDeGeneracion;  // Puntos donde se generarán los NPCs
    public int cantidadDeNPCs = 5;  // Cantidad de NPCs a generar

    void Start()
    {
        GenerarNPCs();
    }

    void GenerarNPCs()
    {
        // Asegúrate de que se haya asignado un prefab
        if (npcPrefab == null)
        {
            Debug.LogError("No se ha asignado un prefab de NPC.");
            return;
        }

        // Asegúrate de que haya puntos de generación
        if (posicionesDeGeneracion.Length == 0)
        {
            Debug.LogError("No se han asignado puntos de generación.");
            return;
        }

        // Generar la cantidad de NPCs deseada
        for (int i = 0; i < cantidadDeNPCs; i++)
        {
            // Elegir una posición aleatoria para el NPC (si tienes más de una posición de generación)
            Transform posicionGeneracion = posicionesDeGeneracion[Random.Range(0, posicionesDeGeneracion.Length)];

            // Instanciar el NPC en la posición seleccionada
            GameObject npcGenerado = Instantiate(npcPrefab, posicionGeneracion.position, Quaternion.identity);

            // Si quieres realizar alguna configuración extra para cada NPC, puedes hacerlo aquí
            // Por ejemplo, si necesitas configurar algo específico en cada NPC:
            // npcGenerado.GetComponent<NPCScript>().ConfiguracionExtra();
        }
    }
}