using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationScript : MonoBehaviour
{
    public Animator animator;
    public Transform modelTransform;
    private float xOrientation = 1;
    private Rigidbody rb;
    public float speed = 5;
    private BoxCollider boxCollider;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (x != 0)
        {
            xOrientation = x;
        }

        animator.SetBool("isWalking", x != 0);
        modelTransform.rotation = Quaternion.Euler(0, xOrientation * 90, 0);
        rb.velocity = new Vector3(speed * x, rb.velocity.y, 0);
    }
}
