using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CinematicCameraMovement : MonoBehaviour {

    public List<Movement> movements;
    public int movIndex = 0;

    [System.Serializable]
    public class OnMovementsComplete : UnityEvent { }

    public OnMovementsComplete whenMovementsComplete = new OnMovementsComplete();

    private void Start()
    {
        if (movements.Count > 0)
        {
            Movement mov = movements[0];
            transform.position = mov.point1.position;
            transform.rotation = Quaternion.Euler(mov.point1.rotation);
        }
    }

    private float currentLerpTime;
    private void Update()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > movements[movIndex].timeToComplete)
        {
            currentLerpTime = movements[movIndex].timeToComplete;
        }

        float t = currentLerpTime / movements[movIndex].timeToComplete;

        switch (movements[movIndex].transitionType)
        {
            case TransitionType.EaseOut:
                t = Mathf.Sin(t * Mathf.PI * 0.5f);
                break;
            case TransitionType.EaseIn:
                t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
                break;
            case TransitionType.Quadratic:
                t = t * t;
                break;
            case TransitionType.SmoothStep:
                t = t * t * (3f - 2f * t);
                break;
            case TransitionType.SmootherStep:
                t = t * t * t * (t * (6f * t - 15f) + 10f);
                break;
            case TransitionType.Linear:
                t = t;
                break;
        }

        transform.position = Vector3.Lerp(movements[movIndex].point1.position, movements[movIndex].point2.position, t);
        transform.rotation = Quaternion.Euler(Vector3.Lerp(movements[movIndex].point1.rotation, movements[movIndex].point2.rotation, t));

        
        if (currentLerpTime == movements[movIndex].timeToComplete)
        {
            if (movements.Count > movIndex + 1)
            {
                movIndex++;
                currentLerpTime = 0f;
            }
            else
            {
                whenMovementsComplete.Invoke();
            }
        }
    }

}

[System.Serializable]
public class Movement
{
    public Waypoint point1;
    public Waypoint point2;

    public float timeToComplete;
    public TransitionType transitionType;
    public AnimationCurve transitionCurve;
}

[System.Serializable]
public class Waypoint
{
    public Vector3 position;
    public Vector3 rotation;
}

[System.Serializable]
public enum TransitionType
{
    EaseIn,
    EaseOut,
    Quadratic,
    SmoothStep,
    SmootherStep,
    Linear,
    CustomCurve
}