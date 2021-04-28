using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject obstacle;
    public float respawnTime;
    //float left = -6f;
    //float right = 7f;
    public int objNum;
    //float prevPos = 0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wave());
    }
    
    private void spawnObstacle()
    {
        
        for(int i = 0; i<objNum; ++i)
        {
            Instantiate(obstacle,new Vector3(Random.Range(250,350),1,175),Quaternion.identity); 
            
        }

    }

    IEnumerator wave()
    {
        while(true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnObstacle();
        }
    }

}
