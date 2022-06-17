using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    public Text AmmoText; // display how much ammo the player has
    public Gun playerGun; // access to the gun for ammo
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AmmoText.text = "Ammo: " + playerGun.ammo; // display ammo
    }
}
