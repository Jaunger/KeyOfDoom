using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class EnemyChaseScript : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject player;
    public UnityEvent onDeath;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        checkSight();
        checkLives();
 
    }

    private void checkLives()
    {
        if (player.GetComponent<FmsScript>().hasBeenCaught && !player.GetComponent<FmsScript>().isBeingChased)
        {
            Debug.Log("Player Caught - Triggering Respawn");
            player.GetComponent<FmsScript>().hasBeenCaught = false;
            player.GetComponent<FmsScript>().respawn(); // Call the respawn method here
            onDeath.Invoke();

            player.GetComponent<FmsScript>().lives--;
            ScoreManager.Instance.RemoveScore(10);
        }
    }

    private void checkSight()
    {
        int count = 0;
        for (int i = 0; i < enemy.Length; i++)
        {
            if (enemy[i].GetComponent<WayPointScript>().isDizzy == false)
            {
                if (enemy[i].GetComponent<WayPointScript>().canSeePlayer)
                {
                    count++;
                    enemy[i].GetComponent<WayPointScript>().target = player.gameObject.transform.position;
                    Debug.Log("Player Spotted");

                    enemy[i].GetComponent<WayPointScript>().onChase();
                }
                else
                {
                    enemy[i].GetComponent<WayPointScript>().onPatrol();
                }
            }
        }
        player.gameObject.GetComponent<FmsScript>().isBeingChased = count > 0;
    }
}