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
    public int playerHealth;
    public Text playerHealthText;

    public Text end;

    public int level2;
    public int level3;
    public int level4;
    public int levelWin;

    public int timeTillLoad;

    public int damageToTake;

    public List<GameObject> SpawnPoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SetZombiesEscaped();
        SetZombiesKilled();
        SetPlayerHealth();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //void to call to set the zombie killed text to display the zombies killed plus how many were killed
    public void SetZombiesKilled()
    {
        zombieKilledText.text = "Zombies Killed: " + zombieKilled.ToString();
        if (zombieKilled >= level2)
        {
            SpawnPoints[2].SetActive(true);
            SpawnPoints[3].SetActive(true);
            if (zombieKilled >= level3)
            {
                SpawnPoints[4].SetActive(true);
                SpawnPoints[5].SetActive(true);
                if (zombieKilled >= level4)
                {
                    SpawnPoints[6].SetActive(true);
                    SpawnPoints[7].SetActive(true);
                    if (zombieKilled <= levelWin)
                    {
                        end.gameObject.SetActive(true);
                        end.text = "You have won the game by killing enough zombies. Reseting Game";
                        StartCoroutine(WaitFortime());
                        SceneManager.LoadScene(0);
                    }
                }
            }
        }
        else
        {
            return;
        }
    }
    //void to call to set the zombie escape text to display the zombies escaped plus how many escaped
    public void SetZombiesEscaped()
    {
        zombieEscapedText.text = "Zombies Escaped: " + zombieEscaped.ToString();
    }
    //void to call to change the players health
    public void SetPlayerHealth()
    {
        playerHealth -= damageToTake;
        playerHealthText.text = "Health: " + playerHealth.ToString();
        if (playerHealth <= 0)
        {
            end.gameObject.SetActive(true);
            end.text = "You have been killed. Reseting Game";
            StartCoroutine(WaitFortime());
            SceneManager.LoadScene(0);
        }
        else
        {
            return;
        }
    }
    IEnumerator WaitFortime()
    {
        yield return new WaitForSeconds(timeTillLoad);
    }
}

