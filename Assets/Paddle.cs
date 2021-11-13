using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Paddle : MonoBehaviour
{
	public float velocity;
	private Rigidbody2D rb;
	public Sprite sprite;
	public Sprite spriteRight;
	public Sprite spriteLeft;
	public GameObject spear;

	public float maxtime = 60;
	private float timer = 0; 

	public int vertical_paddling = 0;
	public int left_paddling = 0;
	public int right_paddling = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Move() {
    	// if (timer > maxtime) {
    		
    	// 	new_obstacle.transform.position = transform.position + new Vector3(Random.Range(-x, x), 0);
    		
    	// 	timer = 0;
    	// }

    	// timer += Time.deltaTime; 

    	if (Input.GetKey("space")) {
    		GameObject new_spear = Instantiate(spear);
    		Destroy(new_spear, 10);
    	}

    	if (Input.GetKey("right")) {
        	rb.velocity = Vector2.up * 0.3f * velocity;
        	rb.velocity += Vector2.right * 0.6f * velocity;
        	if (right_paddling == 0) {
        		timer = 0;
        		GetComponent<SpriteRenderer>().sprite = spriteRight;
        	}
        	right_paddling = 1;
        }
        if (Input.GetKey("left")) {
        	GetComponent<SpriteRenderer>().sprite = spriteLeft;
        	rb.velocity = Vector2.up * 0.3f * velocity;
        	rb.velocity += Vector2.left * 0.6f * velocity;
        	if (left_paddling == 0) {
        		timer = 0;
        		GetComponent<SpriteRenderer>().sprite = spriteLeft;
        	}
        	left_paddling = 1;
        }
        if (Input.GetKey("up")) {
        	rb.velocity = Vector2.up * velocity;
        	if (vertical_paddling == 0) {
        		timer = 0;
        		GetComponent<SpriteRenderer>().sprite = spriteLeft;
        	}
        	vertical_paddling = 1;        	
        }
         if (Input.GetKey("down")) {
        	rb.velocity = Vector2.down * velocity;
        	if (vertical_paddling == 0) {
        		timer = 0;
        		GetComponent<SpriteRenderer>().sprite = spriteLeft;
        	}
        	vertical_paddling = 1;        	
        }
    }

    // Update is called once per frame
    void Update()
    {
    	if (timer > maxtime) {
	        Move();
	    }

        timer += 1;
        if ((timer > maxtime) && vertical_paddling == 2){
        	vertical_paddling = 0;
        	GetComponent<SpriteRenderer>().sprite = sprite;
        }
        else if (timer > (maxtime/2) && vertical_paddling == 1) {
        	vertical_paddling = 2;
        	GetComponent<SpriteRenderer>().sprite = spriteRight;
        }
        else if (timer > (maxtime/2) && vertical_paddling == 0 && (left_paddling == 1 || right_paddling == 1)) {
        	GetComponent<SpriteRenderer>().sprite = sprite;
        	if (left_paddling == 1)
        		left_paddling = 0;
        	else if (right_paddling == 1)
        		right_paddling = 0;
        }
    }

     void OnBecameInvisible() {
         SceneManager.LoadScene( SceneManager.GetActiveScene().name );
     }
}
