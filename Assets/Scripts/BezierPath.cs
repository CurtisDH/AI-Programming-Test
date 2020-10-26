using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BezierPath : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;

    [SerializeField]
    float _t;

    float _speed = 2;
    public Vector3 GetPos(int index)
    {
        return points[index].position;
    }
    bool flip = false;
    void Update()
    {
        if (_t >= 1)
        {
            flip = true;
        }
        else if (_t <= 0)
        {
            flip = false;
        }
        TraversePath(flip);


    }

    void TraversePath(bool flip)
    {
        if(flip == false)
        {
            _t += Time.deltaTime / _speed;
        }

        else
        {
            _t -= Time.deltaTime / _speed;
        }
    }

    public void OnDrawGizmos()
    {
        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawSphere(GetPos(i), 0.1f);
        }
        Handles.DrawBezier(GetPos(0), GetPos(3), GetPos(1), GetPos(2), Color.white, EditorGUIUtility.whiteTexture, 1);
        OrientedPoint bezPoint = GetBezierPoint(_t);

        Handles.PositionHandle(bezPoint.pos, bezPoint.rot);
    }
    public OrientedPoint GetBezierPoint(float t)
    {
        Vector3 p0 = GetPos(0), p1 = GetPos(1), p2 = GetPos(2), p3 = GetPos(3);



        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);
        Vector3 tangent = (e - d).normalized;
        Vector3 pos = Vector3.Lerp(d, e, t);
        return new OrientedPoint(pos, tangent);
    }
}
public struct OrientedPoint
{
    public Vector3 pos;
    public Quaternion rot;
    public OrientedPoint(Vector3 pos, Quaternion rot)
    {
        this.pos = pos;
        this.rot = rot;
    }
    public OrientedPoint(Vector3 pos, Vector3 forward)
    {
        this.pos = pos;
        this.rot = Quaternion.LookRotation(forward);
    }
}
