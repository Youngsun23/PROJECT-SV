using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterResource : MonoBehaviour
{
    public int Value { get; set; }

    public System.Action<int> OnChanged;
}
