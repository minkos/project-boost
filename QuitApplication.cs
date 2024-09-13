using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("You have gave up on the game!");
            Application.Quit(); // Only works when game is built.
        }
    }
}
