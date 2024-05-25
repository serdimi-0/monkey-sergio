using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChatacterAnimatorScript : MonoBehaviour
{
    public Animator animator;
    public Transform modelTransform;
    private float xOrientation = 1;
    private Rigidbody rb;
    public float speed = 5;
    public float runningSpeed = 10;
    public float jumpSpeed = 5;
    public float climbingSpeed = 5;
    public Transform rightHand;
    public GameObject bananaPrefab;

    public Transform leftFoot, rightFoot, headTop;
    private BoxCollider boxCollider;

    private LayerMask groundMask;
    private LayerMask ladderBottomMask;
    private LayerMask ladderTopMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        groundMask = LayerMask.GetMask("Ground");
        ladderBottomMask = LayerMask.GetMask("LadderBottom");
        ladderTopMask = LayerMask.GetMask("LadderTop");
    }

    // Update is called once per frame
    void Update()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Climbing"))
        {
            modelTransform.rotation = Quaternion.Euler(0, 0, 0);
            /*rb.velocity = new Vector3(Input.GetAxis("Horizontal") * climbingSpeed * 0, Input.GetAxis("Vertical") * climbingSpeed, 0);*/
            rb.position += Vector3.up * Input.GetAxis("Vertical") * climbingSpeed * Time.deltaTime;
            animator.SetFloat("climbing_y", Math.Abs(Input.GetAxis("Vertical")) + Math.Abs(Input.GetAxis("Horizontal")) + 0.001f);

            LayerMask target = Input.GetAxis("Vertical") > 0 ? ladderTopMask : ladderBottomMask;

            if (Physics.CheckSphere(rightFoot.position, .1f, target) || Physics.CheckSphere(leftFoot.position, .1f, target))
            {
                rb.isKinematic = false;
                animator.SetBool("isClimbing", false);
            }

        }
        else
        {

            // Gestion escalera

            // nos movemos al estado de climbing
            if ((Input.GetAxis("Vertical") > 0 && IsInLadder && IsBottomLadder) || (Input.GetAxis("Vertical") < 0 && IsInLadder && IsTopLadder))
            {
                rb.isKinematic = true;
                animator.SetBool("isClimbing", true);
            }

            // Gestion salto
            bool isJumping = Input.GetButtonDown("Jump");
            animator.SetBool("isJumping", isJumping);

            animator.SetFloat("yVelocity", rb.velocity.y);

            // Ataque banana
            bool hasThrown = Input.GetButtonDown("Fire1");
            if (hasThrown)
            {
                animator.SetTrigger("hasThrown");
            }


            // Gestion movimento
            float x = Input.GetAxisRaw("Horizontal");

            if (x != 0)
            {
                xOrientation = x;
            }

            bool isRunning = Input.GetButton("run");
            animator.SetBool("isWalking", x != 0);
            animator.SetBool("isRunning", isRunning && x != 0);

            modelTransform.rotation = Quaternion.Euler(0, xOrientation * 90, 0);

            rb.velocity = new Vector3((isRunning ? runningSpeed : speed) * x, rb.velocity.y, 0);

            // Comprobar si el personaje está en el suelo
            bool isGrounded = Physics.CheckSphere(rightFoot.position, 1f, groundMask) || Physics.CheckSphere(leftFoot.position, 1f, groundMask);
            //Debug.Log(isGrounded);
            animator.SetBool("isGrounded", isGrounded);

            // Actualizacion box collider segun la posicion de la cabeza y de los pies
            boxCollider.center = new Vector3(-0.05f, ((headTop.position.y + Mathf.Min(rightFoot.position.y, leftFoot.position.y)) / 2) - transform.position.y - .01f, 0);
            boxCollider.size = new Vector3(.7f, headTop.position.y - Mathf.Min(rightFoot.position.y, leftFoot.position.y), .5f);
        }
    }

    private void FixedUpdate()
    {
    }

    public void ThrowGun()
    {
        GameObject banana = GameObject.Instantiate(bananaPrefab, rightHand.position, Quaternion.identity);
        BananaControllerScript bananaController = banana.GetComponent<BananaControllerScript>();
        bananaController.Init(xOrientation);
    }

    public void JumpEvent()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, 0);
    }

    private bool IsInLadder = false;
    private bool IsTopLadder = false;
    private bool IsBottomLadder = false;

    private void OnTriggerEnter(Collider other)
    {
        CheckTriggers(other, true);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckTriggers(other, false);
    }

    private void CheckTriggers(Collider other, bool active)
    {
        if (other.CompareTag("Ladder"))
        {
            if (other.gameObject.name.Equals("Ladder"))
            {
                IsInLadder = active;
            }
            else if (other.gameObject.name.Equals("LadderTop"))
            {
                IsTopLadder = active;
            }
            else if (other.gameObject.name.Equals("LadderBottom"))
            {
                IsBottomLadder = active;
            }
        }
    }
}
