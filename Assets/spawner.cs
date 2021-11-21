using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

	public float maxtime = 1;
	private float timer = 0; 
	public GameObject obstacle;
    public GameObject fish;
	public float x;

    // Start is called before the first frame update
    void Start()
    {
        obstacle = Resources.Load<GameObject>("GameObjects/rock obj");
        fish = Resources.Load<GameObject>("GameObjects/fish down");
    }

    // Update is called once per frame
    void Update()
    {
    	if (timer > maxtime) {
    		GameObject new_obstacle = Instantiate(obstacle);
    		new_obstacle.transform.position = transform.position + new Vector3(Random.Range(-x, x), 0);
    		Destroy(new_obstacle, 15);

            GameObject new_fish = Instantiate(fish);
    		new_fish.transform.position = transform.position + new Vector3(Random.Range(-x, x), 0);
    		Destroy(new_fish, 15);

    		timer = 0;
    	}

    	timer += Time.deltaTime; 
        
    }
}
