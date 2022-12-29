using System.Collections;
using UnityEngine;

public class SpringToRotationGear : BaseSpringBehaviour, ISpringTo<Vector3>, ISpringTo<Quaternion>, INudgeable<Vector3>, INudgeable<Quaternion>
{
    private SpringVector3 Spring;

    private bool springing = false;
    private Vector3 rootPosition;
    private Vector3 prevValue;

    private void Awake()
    {
        Spring = new SpringVector3()
        {
            StartValue = transform.rotation.eulerAngles,
            EndValue = transform.rotation.eulerAngles,
            Damping = Damping,
            Stiffness = Stiffness
        };

        prevValue = Spring.StartValue;
        rootPosition = Spring.StartValue;
    }

    public void SpringTo(Vector3 TargetRotation)
    {
        SpringTo(Quaternion.Euler(TargetRotation));
    }


    public void SpringTo(Quaternion TargetRotation)
    {
        StopAllCoroutines();

        CheckInspectorChanges();

        StartCoroutine(DoSpringToTarget(TargetRotation));
    }

    private IEnumerator DoSpringToTarget(Quaternion TargetRotation)
    {
        if (Mathf.Approximately(Spring.CurrentVelocity.sqrMagnitude, 0))
        {
            Spring.Reset();
            Spring.StartValue = transform.eulerAngles;
            Spring.EndValue = TargetRotation.eulerAngles;
        }
        else
        {
            Spring.UpdateEndValue(TargetRotation.eulerAngles, Spring.CurrentVelocity);
        }

        // while (Mathf.Abs(1 + Quaternion.Dot(transform.rotation, TargetRotation)) > 0.00001)
        // {
        //     Debug.Log(Spring.CurrentVelocity.sqrMagnitude);
        //     transform.rotation = Quaternion.Euler(Spring.Evaluate(Time.deltaTime));

        //     yield return null;
        // }

        do {
            transform.rotation = Quaternion.Euler(Spring.Evaluate(Time.deltaTime));

            yield return null;
        } while (Mathf.Abs(1 + Quaternion.Dot(transform.rotation, TargetRotation)) > 0.00001 || Mathf.Abs(Spring.CurrentVelocity.y) > 0.001);

        Spring.Reset();
        Spring.StartValue = rootPosition;
        Spring.EndValue = rootPosition;
        prevValue = rootPosition;
        springing = false;
    }

    private void CheckInspectorChanges()
    {
        Spring.Damping = Damping;
        Spring.Stiffness = Stiffness;
    }

    public void Nudge(Vector3 Amount)
    {
        CheckInspectorChanges();
        if (Mathf.Approximately(Spring.CurrentVelocity.sqrMagnitude, 0))
        {
            StartCoroutine(HandleNudge(Amount));
        }
        else
        {
            Spring.UpdateEndValue(Spring.EndValue, Spring.CurrentVelocity + Amount);
        }
    }

    private IEnumerator HandleNudge(Vector3 Amount)
    {
        Spring.Reset();
        Spring.StartValue = transform.rotation.eulerAngles;
        Spring.EndValue = transform.rotation.eulerAngles;
        Spring.InitialVelocity = Amount;
        Quaternion targetRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(Spring.Evaluate(Time.deltaTime));

        while (!Mathf.Approximately(
            0,
            1 - Quaternion.Dot(targetRotation, transform.rotation)
        ))
        {
            transform.rotation = Quaternion.Euler(Spring.Evaluate(Time.deltaTime));

            yield return null;
        }

        Spring.Reset();
    }

    void Update() {
        if (!springing && Vector3.Distance(gameObject.transform.eulerAngles, Spring.StartValue) > 0.2 && (Vector3.Distance(gameObject.transform.eulerAngles, prevValue) < 0.01)) {
            Spring.StartValue = new Vector3(Spring.EndValue.x, -1, Spring.EndValue.z);
            springing = true;
            SpringTo(Spring.StartValue);
        }

        prevValue = gameObject.transform.eulerAngles;
    }

    public void Nudge(Quaternion Amount)
    {
        Nudge(Amount.eulerAngles);
    }
}