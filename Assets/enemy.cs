using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class enemy : MonoBehaviour
{
    public List<Sprite> frames; 

    int frames_per_animation = 90;
    int max_timer;
    int timer;
    
    bool fade_out = false;
    float fade = 1;

    // Start is called before the first frame update
    void Start()
    {   
        max_timer = frames_per_animation * 3 + 1;
        timer = max_timer;
        frames.Add(Resources.Load<Sprite>("Sprites/alligator1"));
        frames.Add(Resources.Load<Sprite>("Sprites/alligator2"));
        frames.Add(Resources.Load<Sprite>("Sprites/alligator3"));
    }

    // Update is called once per frame
    void Update()
    {
        move_animation();
        tick_timer();
    }

    void tick_timer() {
        if (timer == 1) 
            timer = max_timer;
        timer -= 1;
    }

    void move_animation() {
        //every # frames, change the sprite to animate
        if (fade_out) {
             if (timer % frames_per_animation == 0) {
                fade -= 0.3f;
                GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,fade); 
			 }

             if (fade <= 0) {
                Destroy(gameObject);
			 }
                
		}
        else if(timer % frames_per_animation == 0) {
            GetComponent<SpriteRenderer>().sprite = frames[(timer/frames_per_animation)-1];
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.tag == "spear")
        {
            fade_out = true;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Player" && !fade_out)
        {
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        }
    }
}
