using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    Transform[] weapons;

    int _selectedWeapons;

    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel != 0) 
        {
            _selectedWeapons = scrollWheel > 0.0f
                ? _selectedWeapons + 1
                : _selectedWeapons - 1;

            if (_selectedWeapons >= weapons.Length)
            {
                _selectedWeapons = 0;
            }
            else if (_selectedWeapons < 0)
            {
                _selectedWeapons = weapons.Length - 1;
            }
        }

        SelectWeapon();
    }

    private void SelectWeapon()
    {
        int index = 0;
        foreach (var weapon in weapons)
        {
            bool isActive = (index == _selectedWeapons);
            weapon.gameObject.SetActive(isActive);
            index ++;
        }
    }
}
