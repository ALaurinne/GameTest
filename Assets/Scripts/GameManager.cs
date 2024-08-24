using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject inventory;

    bool inventoryOpen;

   
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryOpen = !inventoryOpen;
            inventory.SetActive(inventoryOpen);
        }

    }
}
