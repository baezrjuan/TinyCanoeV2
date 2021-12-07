using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class TitleArrow : MonoBehaviour
{
    public string arrow_direction;
    public GameObject level_icon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        //change title bground
        GameObject title_screen = GameObject.Find("Title screen");
        //title_screen.GetComponent<SpriteRenderer>().sprite = new_sprite;

        //change level button name by selected level
        int level = int.Parse(Regex.Match(level_icon.name, @"\d+").Value);
        if (arrow_direction == "left")
            level_icon.name = "level icon " + (level-1).ToString();
        else if (arrow_direction == "right")
            level_icon.name = "level icon " + (level+1).ToString();
	}
}
