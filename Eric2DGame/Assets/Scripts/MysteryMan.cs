using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MysteryMan : MonoBehaviour
{
    public Text Dialogue; // the text he speaks
    public GameObject Shop; // the shop he sells
    public bool interacting; // when the player is touching him
    public string introDialogue; // what he says when you first touch him
    public string shopDialogue; // what he says when you start buying from his shop
    // Start is called before the first frame update
    void Start()
    {
        Dialogue.gameObject.SetActive(false); // hide his dialogue
        Shop.SetActive(false); // hide his shop
    }

    // Update is called once per frame
    void Update()
    {
        if(interacting == true && Input.GetKeyDown(KeyCode.E))
        {
            Dialogue.text = shopDialogue; // says the next thing
            Shop.SetActive(true); // show shop
        }
    }

    public void BuyAmmo() // button function for shop item
    {
        FindObjectOfType<Gun>().ammo += 10; // give the player some ammo
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            interacting = true;
            Dialogue.gameObject.SetActive(true);
            Dialogue.text = introDialogue; // when we touch the man man he speaks
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            interacting = false;
            Dialogue.gameObject.SetActive(false); // hide his dialogue
            Shop.SetActive(false); // hide his shop
        }
    }
}
