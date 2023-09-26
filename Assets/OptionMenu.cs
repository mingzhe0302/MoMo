using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] GameObject optionMenu;

    public void Pause()
    {
        optionMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        optionMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
