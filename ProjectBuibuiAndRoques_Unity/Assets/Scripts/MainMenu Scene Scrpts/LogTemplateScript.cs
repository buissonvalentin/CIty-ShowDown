using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogTemplateScript : MonoBehaviour
{
    float time;
    public Text content;

    // Start is called before the first frame update
    void Start()
    {
        time = 5;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            Destroy(gameObject);
        }
    }

    
}
