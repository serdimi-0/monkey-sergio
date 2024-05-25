using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatformScript : MonoBehaviour
{
    public Transform[] points;
    public float speed;
    public GameObject player;

    private int currentPoint;
    private Rigidbody rb;
    private Vector3 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentPoint = 0;
        transform.position = points[currentPoint].position;
        GoToNextPoint();
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;

        if (player != null)
            player.transform.position += velocity * Time.deltaTime;

        if ((transform.position - GetTarget()).magnitude < .4)
        {
            currentPoint = (currentPoint + 1) % points.Length;
            GoToNextPoint();
        }
    }

    private void GoToNextPoint()
    {
        Vector3 direction = GetTarget() - GetCurrent();
        velocity = direction.normalized * speed;
    }

    Vector3 GetTarget()
    {
        return points[(currentPoint + 1) % points.Length].position;
    }

    Vector3 GetCurrent()
    {
        return points[currentPoint].position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.GetContact(0).normal == Vector3.down)
                player = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        player = null;
    }


}
