using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

	public float maxtime = 1;
	private float timer = 0; 

    public float maxtime_water = 5;
	private float timer_water = 5; 

    GameObject water;

    public List<GameObject> spawn_objects; 

	public float x;

    // Start is called before the first frame update
    void Start()
    {
        spawn_objects.Add(Resources.Load<GameObject>("GameObjects/rock obj"));
        spawn_objects.Add(Resources.Load<GameObject>("GameObjects/fish down"));
        spawn_objects.Add(Resources.Load<GameObject>("GameObjects/alligator1"));
        water = Resources.Load<GameObject>("GameObjects/water");
    }

    // Update is called once per frame
    void Update()
    {
    	if (timer > maxtime) {
            int alligator_spawn_chance = Random.Range(0, 3);
            foreach(GameObject spawn_object in spawn_objects) {
                if (spawn_object == Resources.Load<GameObject>("GameObjects/alligator1")) {
                    if (alligator_spawn_chance != 1)
                        continue;
                }
                GameObject new_object = Instantiate(spawn_object);
    		    new_object.transform.position = transform.position + new Vector3(Random.Range(-x, x), 0);
    		    Destroy(new_object, 15);
			}
    		timer = 0;
    	}

        if (timer_water > maxtime_water) {
            GameObject new_water = Instantiate(water);
    		new_water.transform.position = transform.position + new Vector3(0,8.2f);
    		Destroy(new_water, 25);

            timer_water = 0;
        }

    	timer += Time.deltaTime; 
        timer_water += Time.deltaTime; 
        
    }
}
