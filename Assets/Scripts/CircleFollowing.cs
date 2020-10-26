using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFollowing : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _wayPoints;
    [SerializeField]
    int _size;

    [SerializeField]
    float _time;
    [SerializeField]
    float _speed = .5f;
    int _wayPointTracker;
    [SerializeField]
    float radius;

    void CreatePath(int points, float radius)
    {
        var angle = 0;
        var angleInc = 360 / points;
        for (int i = 0; i < points; i++)
        {
            angle += angleInc;
            GameObject waypoint = new GameObject("Waypoint" + i);
            waypoint.transform.position = new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad), 0, radius * Mathf.Sin(angle * Mathf.Deg2Rad));
            _wayPoints.Add(waypoint);
        }
        transform.position = _wayPoints[0].transform.position;


    }
    void DrawPath()
    {
        for (int i = 0; i < _wayPoints.Count; i++)
        {
            if (i + 1 >= _wayPoints.Count)
            {
                Debug.DrawLine(_wayPoints[i].transform.position, _wayPoints[0].transform.position, Color.red, Mathf.Infinity);
                break;
            }
            Debug.DrawLine(_wayPoints[i].transform.position, _wayPoints[i + 1].transform.position, Color.red, Mathf.Infinity);
        }
    }
    void Start()
    {
        CreatePath(_size, radius);
        DrawPath();
        //StartCoroutine(TP());
        this.transform.position = FollowPath(_time);
    }
    void Update()
    {

        if (_time > 1)
        {
            _time = 0;
            _wayPointTracker++;
            if (_wayPointTracker >= _wayPoints.Count)
            {
                _wayPointTracker = 0;
            }
        }
        _time += Time.deltaTime * _speed;
        this.transform.position = FollowPath(_time);
    }
    Vector3 FollowPath(float t)
    {
        Vector3 Q;
        if (_wayPointTracker + 1 >= _wayPoints.Count)
        {
            //Debug.Log("##############");
            //Debug.Log("WayPoint.Count" + _wayPointTracker);
            Q = Vector3.Lerp(_wayPoints[_wayPointTracker].transform.position, _wayPoints[0].transform.position, t);
        }
        else
        {
            //Debug.Log("Temp<Count " + _wayPointTracker);
            Q = Vector3.Lerp(_wayPoints[_wayPointTracker].transform.position, _wayPoints[_wayPointTracker + 1].transform.position, t);
        }

        return Q;
    }
}
