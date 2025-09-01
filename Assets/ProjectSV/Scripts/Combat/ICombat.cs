using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActor
{
    GameObject GetActor();
}

public interface IDamage
{
    void TakeDamage(IActor actor, int damage);
}
