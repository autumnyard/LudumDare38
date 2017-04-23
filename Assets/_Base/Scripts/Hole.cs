using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{

    public AudioSource source;
    public AudioClip[] explosions;

    // Use this for initialization
    private void Awake()
    {
        if (source != null)
        {
            source = GetComponent<AudioSource>();
        }

    }
    void Start()
    {
        int which = Random.Range(0, explosions.Length);
        //Debug.Log("which"+ which);
        source.clip = explosions[which];
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
