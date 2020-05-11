using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private float npcSpeed = 2f;

    [SerializeField] private float aggroRadius = 4f;

    [SerializeField] private float turnRadius = 0.1f;

    [SerializeField] private float rayDistance = 3.5f;
    public static float NPCSpeed => Instance.npcSpeed;

    public static float RayDistance => Instance.rayDistance;

    public static float AggroRadius => Instance.aggroRadius;

    public static float TurnRadius => Instance.turnRadius;
    public static GameSettings Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
