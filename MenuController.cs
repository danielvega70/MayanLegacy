using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel; // Reference to your menu panel/canvas

    [SerializeField]
    private GameObject[] itemObjects = new GameObject[8]; // Array para los 8 items

    [SerializeField]
    private GameObject[] itemDisplays = new GameObject[8]; // Referencias a los objetos UI que muestran los items

    [SerializeField]
    private AudioClip[] itemSelectSounds = new AudioClip[8]; // Arreglo de sonidos para cada ítem

    private AudioSource audioSource; // Componente de AudioSource

    private void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();

        // Make sure the menu is hidden when the game starts
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }

        // Ocultar todos los displays de items al inicio
        HideAllItemDisplays();
    }

    private void Update()
    {
        // Check if the I key is being held down
        if (Input.GetKey(KeyCode.I))
        {
            // Show the menu
            if (menuPanel != null)
            {
                menuPanel.SetActive(true);
            }

            // Verificar las teclas numéricas mientras se mantiene I
            for (int i = 0; i < 8; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i) || Input.GetKeyDown(KeyCode.Keypad1 + i))
                {
                    SelectItem(i);
                    PlaySelectSound(i); // Reproducir sonido al seleccionar el ítem
                }
            }
        }
        else
        {
            // Hide the menu when the I key is released
            if (menuPanel != null)
            {
                menuPanel.SetActive(false);
            }
        }
    }

    private void SelectItem(int index)
    {
        if (index >= 0 && index < itemObjects.Length && itemObjects[index] != null)
        {
            Debug.Log($"Item {index + 1} seleccionado");
            // Aquí puedes agregar la lógica de selección del item
            // Por ejemplo, activar el item seleccionado y desactivar los demás

            HideAllItemDisplays();
            if (itemDisplays[index] != null)
            {
                itemDisplays[index].SetActive(true);
            }
        }
    }

    private void PlaySelectSound(int index)
    {
        // Verificar si hay un sonido asignado para este índice
        if (index >= 0 && index < itemSelectSounds.Length && itemSelectSounds[index] != null)
        {
            // Reproducir el sonido correspondiente al ítem seleccionado
            audioSource.PlayOneShot(itemSelectSounds[index]);
        }
    }

    private void HideAllItemDisplays()
    {
        foreach (GameObject display in itemDisplays)
        {
            if (display != null)
            {
                display.SetActive(false);
            }
        }
    }
}