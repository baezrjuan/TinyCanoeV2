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
    public GameObject spear_armed;
    public int spear_speed = 7;

    public GameObject thrownSpear = null;

	public float maxtime = 60;
	private float timer = 0; 

	public int vertical_paddling = 0;
	public int left_paddling = 0;
	public int right_paddling = 0;

    public bool mouseDown = false;
    public Quaternion spear_armed_rotation;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void MoveSpear() {
        if(thrownSpear != null && mouseDown) {
            thrownSpear.transform.position = transform.position;
            //Get the Screen positions of the object
            Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (thrownSpear.transform.position);
         
            //Get the Screen position of the mouse
            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
         
            //Get the angle between the points
            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
 
            //Set spear angle to point towards the mouse position
            spear_armed_rotation = Quaternion.Euler (new Vector3(0f,0f,angle));
            thrownSpear.transform.rotation = spear_armed_rotation;
        }
        else if (thrownSpear != null && !mouseDown) {
            thrownSpear.transform.position += thrownSpear.transform.up * Time.deltaTime * spear_speed;
            Debug.Log("THROWN");
            Debug.Log(thrownSpear.transform.position);
		}
	}
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
         return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void Move() {
    	// if (timer > maxtime) {
    		
    	// 	new_obstacle.transform.position = transform.position + new Vector3(Random.Range(-x, x), 0);
    		
    	// 	timer = 0;
    	// }

    	// timer += Time.deltaTime; 

        //Arm Spear
    	if (Input.GetMouseButtonDown(0) && thrownSpear == null) {
            spear_armed_rotation = Quaternion.Euler (new Vector3(0f,0f,0f));
            Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    		thrownSpear = Instantiate(spear_armed);
            thrownSpear.transform.position = transform.position;
            mouseDown = true;
    	}  
        //Throw Spear
        if (Input.GetMouseButtonUp(0) && mouseDown) {
            Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Destroy(thrownSpear);
            thrownSpear = Instantiate(spear);
            thrownSpear.transform.position = transform.position;
            thrownSpear.transform.rotation = spear_armed_rotation;
            Destroy(thrownSpear,2);
            mouseDown = false;
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
        MoveSpear();

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
