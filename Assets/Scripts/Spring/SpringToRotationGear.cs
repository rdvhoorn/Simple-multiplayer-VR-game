using System.Collections;
using UnityEngine;

public class SpringToRotationGear : BaseSpringBehaviour, ISpringTo<Vector3>, ISpringTo<Quaternion>, INudgeable<Vector3>, INudgeable<Quaternion>
{
    private SpringVector3 Spring;

    private bool springing = false;
    private Vector3 rootRotation;
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
        rootRotation = Spring.StartValue;
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


        do {
            // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Spring.Evaluate(Time.deltaTime)), Time.deltaTime);
            transform.rotation = Quaternion.Euler(Spring.Evaluate(Time.deltaTime));

            yield return null;
        } while (Mathf.Abs(gameObject.GetComponent<Rigidbody>().rotation.eulerAngles.x - rootRotation.x) > 0.1 || Mathf.Abs(Spring.CurrentVelocity.x) > 0.01);

        Debug.Log("Done");
        Spring.Reset();
        Spring.StartValue = rootRotation;
        Spring.EndValue = rootRotation;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        prevValue = rootRotation;
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
        float dist = Mathf.Abs(gameObject.GetComponent<Rigidbody>().rotation.eulerAngles.x - rootRotation.x);
        if (!springing && dist > 10 && (Vector3.Distance(gameObject.transform.eulerAngles, prevValue) < 0.00000001)) {
            Debug.Log("Spring!");
            Spring.StartValue = new Vector3(0, 90, 0);
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