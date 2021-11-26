using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Paddle : MonoBehaviour
{
	public float velocity;
	private Rigidbody2D body;
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
        body = GetComponent<Rigidbody2D>();
        sprite = Resources.Load<Sprite>("Sprites/Boat neutral");
        spriteRight = Resources.Load<Sprite>("Sprites/Boat right");
        spriteLeft = Resources.Load<Sprite>("Sprites/Boat left");
        spear = Resources.Load<GameObject>("GameObjects/spear");
        spear_armed = Resources.Load<GameObject>("GameObjects/spear armed");
    }

    void MoveSpear() {
        //if spear is armed
        if(thrownSpear != null && mouseDown) {
            thrownSpear.transform.position = transform.position;
            //Get the Screen positions of the object
            Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (thrownSpear.transform.position);
         
            //Get the Screen position of the mouse
            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
         
            //Get the angle between the points
            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
 
            //Set spear angle to point towards the mouse position
            spear_armed_rotation = Quaternion.Euler (new Vector3(0f,0f,angle+90f));
            thrownSpear.transform.rotation = spear_armed_rotation;
        }
        //if spear is thrown
        else if (thrownSpear != null && !mouseDown) {
            thrownSpear.transform.position += thrownSpear.transform.up * Time.deltaTime * spear_speed;
		}
	}
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
         return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void Move() {
        Spear_Input();

    	if (Input.GetKey("right")) {
        	body.velocity = Vector2.up * 0.3f * velocity;
        	body.velocity += Vector2.right * 0.6f * velocity;
        	if (right_paddling == 0) {
        		timer = 0;
        		GetComponent<SpriteRenderer>().sprite = spriteRight;
        	}
        	right_paddling = 1;
        }
        if (Input.GetKey("left")) {
        	GetComponent<SpriteRenderer>().sprite = spriteLeft;
        	body.velocity = Vector2.up * 0.3f * velocity;
        	body.velocity += Vector2.left * 0.6f * velocity;
        	if (left_paddling == 0) {
        		timer = 0;
        		GetComponent<SpriteRenderer>().sprite = spriteLeft;
        	}
        	left_paddling = 1;
        }
        if (Input.GetKey("up")) {
        	body.velocity = Vector2.up * velocity;
        	if (vertical_paddling == 0) {
        		timer = 0;
        		GetComponent<SpriteRenderer>().sprite = spriteLeft;
        	}
        	vertical_paddling = 1;        	
        }
         if (Input.GetKey("down")) {
        	body.velocity = Vector2.down * velocity;
        	if (vertical_paddling == 0) {
        		timer = 0;
        		GetComponent<SpriteRenderer>().sprite = spriteLeft;
        	}
        	vertical_paddling = 1;        	
        }
    }

    void Spear_Input() {
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
    }

    void paddle_movement() { 
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

    // Update is called once per frame
    void Update()
    {
        MoveSpear();

    	if (timer > maxtime) {
	        Move();
	    }

        timer += 1;

        paddle_movement();
    }

     void OnBecameInvisible() {
         SceneManager.LoadScene( SceneManager.GetActiveScene().name );
     }
}
