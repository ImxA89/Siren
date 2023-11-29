using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private uint _speed;

    private int _currentTargetIndex = 0;

    private void Update()
    {
       Move();
        
        if(FindOutTargetReached())
            ChangeTarget();
    }

    private void Move() 
    {
        transform.position = Vector3.MoveTowards(transform.position, _points[_currentTargetIndex].position, _speed * Time.deltaTime);
    }

    private bool FindOutTargetReached()
    {
        bool isReached = false;

        if (_points[_currentTargetIndex].position == transform.position)
            isReached = true;

        return isReached;
    }

    private void ChangeTarget()
    {
        _currentTargetIndex++;
        _currentTargetIndex %= _points.Length;
    }
}
