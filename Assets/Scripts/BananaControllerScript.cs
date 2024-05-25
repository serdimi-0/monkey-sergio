using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaControllerScript : MonoBehaviour
{
    public float speed = 5;
    public float rotationSpeed = 5;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    public void Init(float orientation)
    {
        rb = GetComponent<Rigidbody>();
        rb.transform.localScale = new Vector3(orientation * 150, 150, 150);
        rb.velocity = Vector3.right * speed * orientation;
        transform.Rotate(new Vector3(0, rotationSpeed, -rotationSpeed));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
