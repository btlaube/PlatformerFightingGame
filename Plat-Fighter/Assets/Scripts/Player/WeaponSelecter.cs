using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelecter : MonoBehaviour
{
    private List<GameObject> weapons;
    
    private PlayerController playerController;


    void Awake()
    {
        playerController = GetComponent<PlayerController>();

        weapons = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.name == "Hand")
            {
                foreach (Transform weapon in child)
                {
                    weapons.Add(weapon.gameObject);
                }
            }
        }
    }

    void Start()
    {
        SelectWeapon("Shotgun");
    }

    public void SelectWeapon(string name)
    {
        foreach (GameObject weapon in weapons)
        {
            if (weapon.name == name)
            {
                weapon.SetActive(true);
                playerController.weapon = weapon;
            }
            else
            {
                weapon.SetActive(false);
            }
        }
    }




}
