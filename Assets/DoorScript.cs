using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator animator;
    private GameControllerScript gameController;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && gameController.keyNumber > 0)
        {
            animator.SetTrigger("hasOpened");
            gameController.RemoveKey();
            audioSource.Play();
        }
    }
}
