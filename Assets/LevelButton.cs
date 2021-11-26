using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    IDictionary<string, string> levels = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Start()
    {
        levels.Add("1", "Level Demo");
    }

    // Update is called once per frame
    void Update()
    {
        
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
