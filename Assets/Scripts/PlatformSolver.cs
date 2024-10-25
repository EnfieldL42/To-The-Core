using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSolver : MonoBehaviour
{
    [SerializeField] float TraceOfs = 1.0f;// if we need to lift tracing start point
    [SerializeField] float TraceDst = 5.0f;// tracing down distance
    [SerializeField] LayerMask ColisWhat; //set only ground or ship or elevators layer!
    [SerializeField] Rigidbody MyRig;
    

    Transform VirtPoint;
    Vector3 GrndPrevPos;
    float GrndPrevRotY;

    float TraceTimer = 0.0f;
    float PowerOfAligment = 0.0f;

    void LateUpdate()
    {
        if (VirtPoint == null) //create if point is lost
        {
            GameObject temp = new GameObject("vptp");
            VirtPoint = temp.transform;
        }

        TraceTimer += Time.deltaTime;
        if (TraceTimer > 0.5f)//trace from character to down sometime
        {
            TraceTimer = 0.0f;

            RaycastHit hit;
            if (Physics.Raycast(MyRig.position + Vector3.up * TraceOfs, Vector3.down, out hit, TraceDst, ColisWhat))
                if (hit.collider != null)
                {
                    if (VirtPoint.parent != hit.collider.transform) // check if we change platform we staying in.
                    {
                        VirtPoint.parent = hit.collider.transform; // set virtual point as child of ship / elevator or just ground
                        GrndPrevPos = VirtPoint.position = MyRig.position; // reset virtual point to current sharacter position

                        //do same for rotation
                        GrndPrevRotY = MyRig.rotation.eulerAngles.y;
                        VirtPoint.rotation = MyRig.rotation;
                    }
                    PowerOfAligment = 2.5f;
                }

        }

        //power of platform aligment will be ceepet at 2.5 sec every 0.5 sec, so we have 2.0 seconds to fade off platform aligment power;
        if (PowerOfAligment > 0.0f) PowerOfAligment -= Time.deltaTime;

        if (GrndPrevPos != VirtPoint.position) // if platform under characters legs is moved in any direction
        {
            float align = Mathf.Clamp01(PowerOfAligment * 0.5f); // it will fade off aligment power first 2 seconds of legs lost contact with platform (if character jumps from platform - it will work as impulse illusion )

            //we perform move character on that delta. (so we dont need move it if there is no moving at all)
            Vector3 delta = VirtPoint.position - GrndPrevPos;
            MyRig.MovePosition(MyRig.position + delta * align);
        }

        //same for rotations.
        if (GrndPrevRotY != VirtPoint.rotation.eulerAngles.y)
        {
            float align = Mathf.Clamp01(PowerOfAligment * 0.5f); // it will fade off aligment power first 2 seconds of legs lost contact with platform (if character jumps from platform - it will work as impulse illusion )

            Vector3 wasRot = MyRig.rotation.eulerAngles;
            float delta = VirtPoint.eulerAngles.y - GrndPrevRotY;
            if (delta > 180.0f) delta -= 360.0f;
            if (delta < -180.0f) delta += 360.0f;
            wasRot.y += delta * align;

            MyRig.MoveRotation(Quaternion.Euler(wasRot));
        }

        //prepearing data for next frame
        GrndPrevPos = VirtPoint.position = MyRig.position;
        GrndPrevRotY = MyRig.rotation.eulerAngles.y;
        VirtPoint.rotation = MyRig.rotation;
    }
}