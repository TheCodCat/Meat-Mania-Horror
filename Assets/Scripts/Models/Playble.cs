using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class Playble
{
    public PlayableDirector PlayableDirector;
    public List<GameObject> GameObjects;

    public void PlayableDisable()
    {
        GameObjects.ForEach(gameObject => gameObject.SetActive(false));
    }
}