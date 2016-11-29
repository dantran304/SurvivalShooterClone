using UnityEngine;
using System.Collections;

public class MoveToPositionByCurve : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    float step = 0;

    [SerializeField]
    Transform origin, target;

    float timeTracking = 0;
    void Start()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        timeTracking = (distance / moveSpeed);
        Debug.Log(timeTracking);
    }

    void Update()
    {
        MoveToTargetByCurve();

        timeTracking = Mathf.MoveTowards(timeTracking, 0, Time.deltaTime);
        if (timeTracking == 0)
        {
            Debug.Log("da den dich!");
        }
    }

    void MoveToTargetByCurve()
    {
        //move to target
        step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
