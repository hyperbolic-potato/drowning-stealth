using UnityEngine;

public class PlayerNoise : Noise
{
    public PlayerMovement pm;

    new void Start()
    {
        pm = GetComponentInParent<PlayerMovement>();

        base.Start();
    }

    new void Update()
    {
        if (pm != null) radius = pm.noise;
        base.Update();
    }
}
