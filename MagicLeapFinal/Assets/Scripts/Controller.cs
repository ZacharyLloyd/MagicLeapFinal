using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    //ints for the counter and the text for later use
    public int zombieKilled;
    public int zombieEscaped;
    public Text zombieKilledText;
    public Text zombieEscapedText;

    // Start is called before the first frame update
    void Start()
    {
        SetZombiesEscaped();
        SetZombiesKilled();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //void to call to set the zombie killed text to display the zombies killed plus how many were killed
    public void SetZombiesKilled()
    {
        zombieKilledText.text = "Zombies Killed: " + zombieKilled.ToString();
    }
    //void to call to set the zombie escape text to display the zombies escaped plus how many escaped
    public void SetZombiesEscaped()
    {
        zombieEscapedText.text = "Zombies Escaped: " + zombieEscaped.ToString();
    }
}

