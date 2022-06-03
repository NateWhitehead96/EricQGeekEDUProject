using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool inDoor; // to know when the player is in the door
    public GameObject otherDoor; // the other door thats "attached" to this one
    public SpriteRenderer sprite; // image of the door
    public Sprite openDoorSprite; // the sprite image we'll switch the door to
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inDoor == true && Input.GetKeyDown(KeyCode.E))
        {
            player.transform.position = otherDoor.transform.position;
            sprite.sprite = openDoorSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { // the player enters the door
        if (collision.gameObject.GetComponent<Player>())
        {
            inDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    { // the player exits the door
        if (collision.gameObject.GetComponent<Player>())
        {
            inDoor = false;
        }
    }
}
