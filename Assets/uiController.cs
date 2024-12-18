using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiController : MonoBehaviour
{
    public GameObject settingsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        settingsCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the active state of the settings canvas
            if (settingsCanvas != null)
            {
                settingsCanvas.SetActive(!settingsCanvas.activeSelf);
            }
            else
            {
                Debug.LogWarning("Settings Canvas is not assigned in the inspector!");
            }
        }
    }

    public void resumeButton()
    {
        settingsCanvas.SetActive(false);
    }

    public void quitButton()
    {
        Application.Quit();
    }
}
