using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFullScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen= true;
        Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
