using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringAxelRotation : BaseSpringBehaviour, ISpringTo<float>
{
    [Range(0, 2)]
    public int axis = 0;


    private AxalSpringVector Spring;

    private bool springing = false;
    private float rootRotation;
    private float previousRotation;

    private void Awake()
    {
        Spring = new AxalSpringVector()
        {
            StartValue = get_axis_value_from_vector(transform.rotation.eulerAngles),
            EndValue = get_axis_value_from_vector(transform.rotation.eulerAngles),
            Damping = Damping,
            Stiffness = Stiffness
        };

        previousRotation = Spring.StartValue;
        rootRotation = Spring.StartValue;
    }

    public void SpringTo(float TargetRotation)
    {
        StopAllCoroutines();

        CheckInspectorChanges();

        StartCoroutine(DoSpringToTarget(TargetRotation));
    }

    private IEnumerator DoSpringToTarget(float TargetRotation)
    {
        if (Mathf.Approximately(Spring.CurrentVelocity, 0))
        {
            Spring.Reset();
            Spring.StartValue = transform.eulerAngles.x;
            Spring.EndValue = TargetRotation;
        }
        else
        {
            Spring.UpdateEndValue(TargetRotation, Spring.CurrentVelocity);
        }

        do {
            float rotation = Spring.Evaluate(Time.deltaTime);

            // gameObject.GetComponent<Rigidbody>().MoveRotation(rotation_float_to_rotation_quaternion(rotation - previousRotation));
            // transform.Rotate(-rotation, 0, 0, Space.Self);
            Vector3 currentEuler = transform.rotation.eulerAngles;
            currentEuler.x += rotation;
            Debug.Log(rotation);
            transform.rotation = Quaternion.Euler(currentEuler.x, currentEuler.y, currentEuler.z);
            // Debug.Log(Spring.CurrentVelocity);

            yield return null;
        } while (Mathf.Abs(get_axis_value_from_vector(transform.rotation.eulerAngles) - rootRotation) > 0.1 || Mathf.Abs(Spring.CurrentVelocity) > 0.01);

        Debug.Log("Done");
        Spring.Reset();
        Spring.StartValue = rootRotation;
        Spring.EndValue = rootRotation;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        previousRotation = rootRotation;
        springing = false;
    }

    private void CheckInspectorChanges()
    {
        Spring.Damping = Damping;
        Spring.Stiffness = Stiffness;
    }


    void Update() {
        float currentRotation = get_axis_value_from_vector(transform.rotation.eulerAngles);
        float diffToStandardRotation = Mathf.Abs(currentRotation - rootRotation);

        if (!springing && diffToStandardRotation > 0.2 && Mathf.Abs(currentRotation - previousRotation) < 0.001) {
            Debug.Log(currentRotation);
            Debug.Log(diffToStandardRotation);
            Debug.Log("Spring!");
            Spring.StartValue = currentRotation;
            springing = true;
            SpringTo(Spring.EndValue);
        }

        previousRotation = currentRotation;
    }


    private float get_axis_value_from_vector(Vector3 vector) {
        if (axis == 0) {
            return vector.x;
        } else if (axis == 1) {
            return vector.y;
        } else {
            return vector.z;
        }
    }

    private float get_axis_value_from_quaternion(Quaternion quat) {
        if (axis == 0) {
            return quat.x;
        } else if (axis == 1) {
            return quat.y;
        } else {
            return quat.z;
        }
    }

    private Quaternion rotation_float_to_rotation_quaternion(float rotation) {
        Quaternion current = gameObject.GetComponent<Rigidbody>().rotation;

        return Quaternion.RotateTowards(current, Quaternion.Euler(rotation, 0, 0), 1);
    }
}
