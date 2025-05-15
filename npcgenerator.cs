using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcgenerator : MonoBehaviour
{
    public GameObject npcPrefab;  // Prefab del NPC que se usar� para generar nuevos NPCs
    public Transform[] posicionesDeGeneracion;  // Puntos donde se generar�n los NPCs
    public int cantidadDeNPCs = 5;  // Cantidad de NPCs a generar

    void Start()
    {
        GenerarNPCs();
    }

    void GenerarNPCs()
    {
        // Aseg�rate de que se haya asignado un prefab
        if (npcPrefab == null)
        {
            Debug.LogError("No se ha asignado un prefab de NPC.");
            return;
        }

        // Aseg�rate de que haya puntos de generaci�n
        if (posicionesDeGeneracion.Length == 0)
        {
            Debug.LogError("No se han asignado puntos de generaci�n.");
            return;
        }

        // Generar la cantidad de NPCs deseada
        for (int i = 0; i < cantidadDeNPCs; i++)
        {
            // Elegir una posici�n aleatoria para el NPC (si tienes m�s de una posici�n de generaci�n)
            Transform posicionGeneracion = posicionesDeGeneracion[Random.Range(0, posicionesDeGeneracion.Length)];

            // Instanciar el NPC en la posici�n seleccionada
            GameObject npcGenerado = Instantiate(npcPrefab, posicionGeneracion.position, Quaternion.identity);

            // Si quieres realizar alguna configuraci�n extra para cada NPC, puedes hacerlo aqu�
            // Por ejemplo, si necesitas configurar algo espec�fico en cada NPC:
            // npcGenerado.GetComponent<NPCScript>().ConfiguracionExtra();
        }
    }
}