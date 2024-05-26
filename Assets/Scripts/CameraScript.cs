using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public int zOffset = -23;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {

        float yOffSet = target.name.Contains("Trans") ? 1.1f : 2.1f;

        Vector3 targetPos = new Vector3(target.position.x, target.position.y + yOffSet, zOffset);
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.1f);
    }
    
}
