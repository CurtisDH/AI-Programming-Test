using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAI : MonoBehaviour
{
    [SerializeField]
    float _radius;
    [SerializeField]
    float _Maxangle;
    [SerializeField]
    float _speed;
    Vector3 _targetPos;
    Vector3 currentPos;
    Vector3 GetTargetPos()
    {

        Vector3 pos = Random.insideUnitSphere * _radius;
        pos.y = 0;
        return pos;
    }
    Vector3 MoveToTargetPos(Vector3 pos)
    {
        Debug.DrawLine(transform.position, pos, Color.green);

        var tPos = transform.position += (_targetPos - transform.position).normalized * _speed * Time.deltaTime;


        return tPos;
    }
    Quaternion LookAtTarget()
    {
        return Quaternion.LookRotation(MoveToTargetPos(_targetPos));
        
    }
    void Start()
    {
        currentPos = transform.position;
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, _targetPos) < 0.2f)
        {
            currentPos = transform.position;
            _targetPos = GetTargetPos();
            Debug.Log(Vector3.Distance(transform.position, _targetPos));
        }
        this.transform.position = MoveToTargetPos(_targetPos);
        transform.rotation = LookAtTarget();


    }


}
