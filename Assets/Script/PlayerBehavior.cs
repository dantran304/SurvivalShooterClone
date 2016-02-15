using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

    public float moveSpeed;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigid;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigid = GetComponent<Rigidbody>();
    }


    // dung cho physic
    void FixedUpdate()
    {
        float mx = Input.GetAxisRaw("Horizontal");
        float mz = Input.GetAxisRaw("Vertical");
        Move(mx, mz);
        Turning();
        Animating(mx, mz);
    }

    // ham di chuyen
    void Move(float x, float z) 
    {
        // set movement vector
        movement.Set(x, 0f, z);

        //deltatime = time between update call
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        playerRigid.MovePosition(transform.position + movement);
    }

    // quay player = chuot
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            playerRigid.MoveRotation(newRotation);
        }
    }

    //dieu khien animation
    void Animating(float x, float z)
    {
        bool walking = x != 0f || z != 0f;
        anim.SetBool("isMove",walking);
    }
}
