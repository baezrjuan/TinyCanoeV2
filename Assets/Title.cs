using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Title : MonoBehaviour
{
    public GameObject level_icon;

    GameObject title_screen;

    public List<Sprite> title_screens; 

    // Start is called before the first frame update
    void Start()
    {
        title_screens.Add(Resources.Load<Sprite>("Sprites/level title 1"));
        title_screens.Add(Resources.Load<Sprite>("Sprites/level title 2"));

        title_screen = GameObject.Find("Title screen");
    }

    // Update is called once per frame
    void Update()
    {
        int level = int.Parse(Regex.Match(level_icon.name, @"\d+").Value);
        title_screen.GetComponent<SpriteRenderer>().sprite = title_screens[level-1];
    }
}
