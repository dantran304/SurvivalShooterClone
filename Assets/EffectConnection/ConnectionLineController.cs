using UnityEngine;
using System.Collections;

public class ConnectionLineController : MonoBehaviour
{
    public LineRenderer line;

    public Transform startPoint;
    public Transform endPoint;

    [SerializeField]
    private GameObject startLineEffect;

    [SerializeField]
    private GameObject endLineEffect;


    // Use this for initialization
    void Start()
    {
        SetupLine();
    }

    // Update is called once per frame


    void SetupLine()
    {
        line.SetPosition(0, startPoint.position);
        line.SetPosition(1, endPoint.position);

        startLineEffect.SetActive(true);
        endLineEffect.SetActive(true);
        startLineEffect.transform.position = startPoint.position;
        endLineEffect.transform.position = endPoint.position;
    }

    void ResetLine()
    {
        startLineEffect.SetActive(false);
        endLineEffect.SetActive(false);
    }
}
