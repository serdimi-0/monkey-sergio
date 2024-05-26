using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeletransportationScript : MonoBehaviour
{
    public GameObject destination;
    private GameControllerScript gameController;
    private bool canTeleport = false;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
    }

    void Update()
    {
        if (canTeleport && Input.GetKeyDown(KeyCode.F))
            player.transform.position = destination.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameController.ShowHint("Press F to teleport");

        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            canTeleport = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canTeleport = false;
    }
}
