using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fish : MonoBehaviour
{
    public List<Sprite> frames; 

    int frames_per_animation = 60;
    int max_timer;
    int timer;

    int max_score = 1;

    // Start is called before the first frame update
    void Start()
    {   
        max_timer = frames_per_animation * 3 + 1;
        timer = max_timer;
        frames.Add(Resources.Load<Sprite>("Sprites/fish down1"));
        frames.Add(Resources.Load<Sprite>("Sprites/fish down2"));
        frames.Add(Resources.Load<Sprite>("Sprites/fish down3"));
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
        //every # frames, change the fish sprite to animate
        if (timer % frames_per_animation == 0 && GetComponent<SpriteRenderer>().sprite != Resources.Load<Sprite>("Sprites/fish caught")) {
            GetComponent<SpriteRenderer>().sprite = frames[(timer/frames_per_animation)-1];
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.tag == "spear")
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/fish caught");
            Destroy(gameObject, 1);

            Destroy(collision.gameObject);

            GameObject score_obj = GameObject.Find("Fish Score");
            string score_txt = score_obj.GetComponent<Text>().text;
            int score = (int.Parse(score_txt) + 1);
            score_obj.GetComponent<Text>().text = score.ToString();
            
            if (score == max_score) {
                SceneManager.LoadScene("Scenes/title screen", LoadSceneMode.Single); 
			}
        }
    }
}
