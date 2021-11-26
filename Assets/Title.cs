using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    float timer = 0;
    float max_time = 2;

    public List<Sprite> frames; 
    float fade = 1;
    bool fade_out;

    // Start is called before the first frame update
    void Start()
    {
        frames.Add(Resources.Load<Sprite>("Sprites/Title screen"));
        frames.Add(Resources.Load<Sprite>("Sprites/Title screen 2"));

        GetComponent<SpriteRenderer>().sprite = frames[0];
    }

    // Update is called once per frame
    void Update()
    {
        Animator();
    }

    void Animator() {
        if (fade_out)
            fade -= 0.004f;
        else if (!fade_out)
            fade += 0.004f;

        GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,fade); 
        if (fade >= 1)
            fade_out = true;
        else if (fade <= 0)
            fade_out = false; //fade-in
            

	}
}
