using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrel : MonoBehaviour
{

    public float speed;
    private Rigidbody rb;
    float time;
    float seconds;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        //StartCoroutine(wait());
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0,0,-speed);
        

        if(transform.position.z < 60)
        {
            Destroy(this.gameObject);
        }
    }
    
}
