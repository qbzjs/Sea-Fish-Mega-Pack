﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlatformManager : MonoBehaviour
{
      /*
      * Author: Matthew Thompson, 10/3/20
      * In this implementation platforms are destroyed when passed and instantiated each time they are needed.
      * ideally the goal is to pre-generate a few of each platform and then simply move those instances, rather 
      * than needing to destro/instantiate them each time, for efficiency.
      */

    [SerializeField] private GameObject[] platformPrefabs; // a set of prefabs from which to create platforms
    [SerializeField] private List<GameObject> platformInstances = new List<GameObject>(); // instances of platforms created, currently unused
    [SerializeField] private bool[] recentlyUsed; // an array of bools to mark if a platform has been used in the last 1/2 generations; prevents duplicates

    [SerializeField] private float prefabLength = 100; // lengrh of platforms/level chunks
    private float offset = 0; // distnace between chunks
    [SerializeField] private int minPlatformNum = 8; // number of platrorms generated at start, minimum number in play at a given time
    [SerializeField] private int minNumOfEach = 3; // minimum number of each platform to be instantiated, currently unused.
    private int platformNum = -1;

    // Start is called before the first frame update
    void Start()
    {
        // Used to pre-generate a few instances of each type of platform
        //for (int x = 0; x < platformPrefabs.Length; x++)
        //{
        //    for (int y = 0; y < minNumOfEach; y++)
        //    {
        //        platformInstances.Add(Instantiate(platformPrefabs[x], new Vector3(0, 0, -100 * x), Quaternion.Euler(0, 0, 0)));
        //    }
        //}

        // creates platforms until the minimum number have been created
        for (int i = 0; i < minPlatformNum; i++)
        {
            CreatePlatform();
        }
    }

    // creates a new instance of a platform at the current offset, chooses randomly and ensures that prefabs
    // do not show up twice in a row or with fewer than 2 different prefabs in betweem
    public void CreatePlatform()
    {
        int secondMostRecent = -1; // the 2nd most recently created platform, -1 so as not to cause issues on the first generation
        if (platformNum != -1) // if this is not the first generation of platforms
        {
            secondMostRecent = platformNum; // there have been at least two platforms created, 2nd most recent kept track of
        }

        // picks a random prefab to make, keeps choosing until it settles on a prefab that has not been used the past 2 times
        platformNum = Random.Range(0, platformPrefabs.Length);
        while (recentlyUsed[platformNum] == true)
        {
            platformNum = Random.Range(0, platformPrefabs.Length);
        }

        // instantiates a platform at the given offset
        Instantiate(platformPrefabs[platformNum], new Vector3(0, 0, offset), Quaternion.Euler(0, 0, 0));

        // resets all recently used bools to false
        for (int q = 0; q < recentlyUsed.Length; q++)
        {
            recentlyUsed[q] = false;
        }

        // sets 1st and 2nd most recently used bools to true
        if (secondMostRecent != -1)
        {
            recentlyUsed[secondMostRecent] = true;
        }
        recentlyUsed[platformNum] = true;

        // increments offset
        offset += prefabLength;
    }

    // originally simply moved an object when out of view, now deletes it and creates a new platform
    public void RecyclePlatform(GameObject platform)
    {
        // user for moving a platform to the current offset
        //platform.transform.position = new Vector3(0, 0, offset);
        //offset += prefabLength;

        // deletes platorm once it is off screen, makes a new one
        Destroy(platform);
        CreatePlatform();
    }

}