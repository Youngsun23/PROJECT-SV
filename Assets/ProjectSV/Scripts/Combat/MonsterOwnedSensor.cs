using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOwnedSensor : MonoBehaviour
{
    private CircleCollider2D sensorCollider;
    [SerializeField] private float detectRadius;

    public System.Action<PlayerCharacter> OnDetectedTarget;
    public System.Action<PlayerCharacter> OnLostTarget;

    public void SetSensorDetectRadius(float val)
    {
        detectRadius = val;
    }

    private void Awake()
    {
        sensorCollider = GetComponent<CircleCollider2D>();
        sensorCollider.isTrigger = true;
        sensorCollider.radius = detectRadius;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerCharacter playerCharacter))
        {
            OnDetectedTarget?.Invoke(playerCharacter);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerCharacter playerCharacter))
        {
            OnLostTarget?.Invoke(playerCharacter);
        }
    }
}
