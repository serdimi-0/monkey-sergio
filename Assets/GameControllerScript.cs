using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public GameObject characterPrefab;
    public GameObject transformationPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //e key pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject player = GameObject.Find("Character");
            Destroy(player);
            Instantiate(transformationPrefab, player.transform.position, player.transform.rotation);
/*            // remove player object and add transformation object
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                Destroy(player);
                Instantiate(transformationPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
            {
                // remove transformation object and add player object
                GameObject transformation = GameObject.Find("Transformation");
                if (transformation != null)
                {
                    Destroy(transformation);
                    Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                }
            }*/
        }
    }
}
