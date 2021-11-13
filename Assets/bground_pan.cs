using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bground_pan : MonoBehaviour
{
	public int maxtime = 60;
	public int timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	if (timer > maxtime) {
	    	Vector3 position = this.transform.position;
	    	position.y -= 0.01f;
	    	this.transform.position = position; 
	    }
	    timer += 1;       
    }
}
