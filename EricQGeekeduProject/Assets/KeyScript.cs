using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject Goal;

    private void Update()
    {
        transform.Rotate(Vector3.up * 1 * Time.deltaTime); // this rotates the key around the y-axis
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Goal.SetActive(true);
            Destroy(gameObject);
        }
    }
}
