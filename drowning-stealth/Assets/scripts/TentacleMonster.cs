using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public class TentacleMonster : MonoBehaviour
{
    public GameObject prefab;
    public TentacleAttack[] attacks;
    public GameObject[] tentacles;

    private void Start()
    {
        ResetAttacks();
    }

    void ResetAttacks()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i));
        }

        for (int i = 0;i < tentacles.Length; i++)
        {
            GameObject temp = Instantiate(tentacles[i]);
        }
    }
}
