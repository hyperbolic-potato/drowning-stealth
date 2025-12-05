using UnityEngine;
using System.Collections;

public class TentacleAttack : MonoBehaviour
{
    public float delay = 0f;
    public float interval = 1f;
    public Vector2 direction = -Vector2.up;
    public float initiative = 0.5f;
    public float speed = 1f;
    public bool isRepeating;

    private void Start()
    {
        StartCoroutine(AttackLoop());
    }

    IEnumerator AttackLoop()
    {
        yield return new WaitForSeconds(delay);

        do
        {
            //attack sequence here

            yield return new WaitForSeconds(interval);
        }
        while (isRepeating);
    }

}
