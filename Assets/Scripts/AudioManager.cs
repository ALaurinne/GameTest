using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio Manager")]
public class AudioManager : ScriptableObject
{
    public AudioClip[] BGM;

    public AudioClip Win;

    public AudioClip Collect;
}
