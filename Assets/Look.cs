using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
   /*
   public float sensitivity = 100f;
   
   public Transform playerBody;
   
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float xVal = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float yVal = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        
        playerBody.Rotate(Vector3.up * xVal);
    }*/
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    
    void FixedUpdate() {
       Vector3 desiredPosition = target.position + offset;
       Vector3 smoothedPostition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
       transform.position = smoothedPostition;
    }
}
