using UnityEngine;
using System.Collections;

public class RespawnScript : MonoBehaviour
{

    public GameObject prefabThing;
    public int countdown = 60;
    public int startCountdown;

    // Use this for initialization
    void Start()
    {
        startCountdown = countdown;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        countdown--;
        if (countdown < 1)
        {
            countdown = startCountdown;
            RespawnFun();
        }
    }

    void RespawnFun()
    {
        // Instantiates respawnPrefab at the location 
        // of the game object with tag "Respawn"
        //Found on http://docs.unity3d.com/Documentation/Components/Tags.html

        GameObject respawn = GameObject.FindWithTag("Respawn");
        Instantiate(prefabThing, respawn.transform.position, respawn.transform.rotation);
    }

    GameObject FindClosestGameObjectWithTag(string tagToFind)
    {
        GameObject result = null;
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag(tagToFind);

        foreach (GameObject current in allObjects)
        {
            if (current != this)
            {
                if (result == null)
                {
                    result = current;
                } 
                else
                {
                    if (Vector3.Distance(transform.position, result.transform.position) > Vector3.Distance(transform.position, current.transform.position))
                    {
                        result = current;
                    }
                }
            }
        }
        return result;
    }

}
