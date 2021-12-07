using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    IDictionary<string, string> levels = new Dictionary<string, string>();
    float fade = 1f;
    string fade_type;

    // Start is called before the first frame update
    void Start()
    {
        // Add level num and scene name
        levels.Add("1", "Level Demo");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,fade); 
        if (fade <= 0f)
            fade_type = "in";
        else if (fade >= 1f)
            fade_type = "out";

        if (fade_type == "out")
            fade -= .002f;
        else if (fade_type == "in")
            fade +=  .005f;
    }

    void OnMouseDown()
    {
        Debug.Log("LOADING Level...");

        foreach(var level in levels)
        {
          if (gameObject.name.Contains(level.Key)) {
            Debug.Log("Lv" + level.Key + ": " + level.Value);
            SceneManager.LoadScene("Scenes/"+level.Value, LoadSceneMode.Single); 
		  }
        }
    }
}
