using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UserDataManagerCore

public interface IPersistant
{
    public string Read();
    public void Load(string jsonString);
}
