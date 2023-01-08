using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxalSpringVector : BaseSpring<float>
{
    private FloatSpring Spring = new();

    public override float Damping
    {
        get { return base.Damping; }
        set
        {
            Spring.Damping = value;
            base.Damping = value;
        }
    }

    public override float Stiffness
    {
        get { return base.Stiffness; }
        set
        {
            Spring.Stiffness = value;
            base.Stiffness = value;
        }
    }

    public override float Evaluate(float DeltaTime)
    {
        CurrentValue = Spring.Evaluate(DeltaTime);

        CurrentVelocity = Spring.CurrentVelocity;

        return CurrentValue;
    }

    public override void Reset()
    {
        Spring.Reset();
    }

    public override void UpdateEndValue(float Value, float Velocity)
    {
        Spring.UpdateEndValue(Value, Velocity);
    }
}