using UnityEngine;

public class CameraControllerX : MonoBehaviour
{
    public BallControllerX target;
    private float offSet;

    // Start is called before the first frame update
    void Awake()
    {
        offSet = transform.position.y - target.transform.position.y;



    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curPos = transform.position;
        curPos.y = target.transform.position.y + offSet;
        transform.position = curPos;

    }
}
