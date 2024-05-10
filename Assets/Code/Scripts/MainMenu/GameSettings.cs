using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings")]
public class GameSettings: ScriptableObject   
{
    [field:SerializeField()]
    public float MasterVolume { get; set; }
}
