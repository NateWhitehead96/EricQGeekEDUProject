using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWin : MonoBehaviour
{
    public GameObject promptText; // the press e to ineract so we can hide and show it
    public Text Description; // the text that actually has meaning

    public bool atConsole; // to know when we're touching the console
    // Start is called before the first frame update
    void Start()
    {
        promptText.SetActive(false);
        Description.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(atConsole == true)
        {
            promptText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Cursor.lockState = CursorLockMode.None; // no longer stuck in the middle
                Cursor.visible = true; // show cursor
                Description.gameObject.SetActive(true);
                atConsole = false; // hide the press e stuff
                promptText.SetActive(false);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        atConsole = true; // knows we are at console
    }

    public void YesBlowUpPlanet()
    {
        Description.text = "The end u ded.";
    }
    public void NoSavePlanet()
    {
        Description.text = "The end planet still killed u.";
    }
}
