using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    public List<Sprite> fish_frames; 

    int max_timer;
    int timer;
    int frames_per_animation = 60;

    // Start is called before the first frame update
    void Start()
    {   
        max_timer = frames_per_animation * 3 + 1;
        timer = max_timer;
        fish_frames.Add(Resources.Load<Sprite>("Sprites/fish down1"));
        fish_frames.Add(Resources.Load<Sprite>("Sprites/fish down2"));
        fish_frames.Add(Resources.Load<Sprite>("Sprites/fish down3"));
    }

    // Update is called once per frame
    void Update()
    {
        //every 30 frames, change the fish sprite to animate
        if (timer % frames_per_animation == 0) {
            GetComponent<SpriteRenderer>().sprite = fish_frames[(timer/frames_per_animation)-1];
        }
        if (timer == 1) 
            timer = max_timer;
        timer -= 1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.tag == "spear")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);

            GameObject score_obj = GameObject.Find("Fish Score");
            string score_txt = score_obj.GetComponent<Text>().text;
            score_obj.GetComponent<Text>().text = (int.Parse(score_txt) + 1).ToString();

        }
    }
}
