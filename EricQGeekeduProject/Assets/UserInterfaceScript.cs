using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceScript : MonoBehaviour
{

    public Text AmmoText;
    public PlayerScript player;
    public Slider HealthSlider;
    // Start is called before the first frame update
    void Start()
    {
        HealthSlider.maxValue = player.Health; // sets the max value
    }

    // Update is called once per frame
    void Update()
    {
        AmmoText.text = player.CurrentAmmo + " / " + player.MaxAmmo; // this text will now display our current ammo out of our max ammo
        HealthSlider.value = player.Health; // update the value of the slider with our health
    }
}
