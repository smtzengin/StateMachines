using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class ProjectileToTargetPrediction : Unit
{
    [DoNotSerialize]
    public ValueInput directionToTarget;
    [DoNotSerialize]
    public ValueInput targetForwardDirection;
    [DoNotSerialize]
    public ValueInput targetSpeed;
    [DoNotSerialize]
    public ValueInput thisTransform;
    [DoNotSerialize]
    public ValueInput projectileSpeed;

    [DoNotSerialize] public ValueOutput directionToPredictedPosition;
    private Vector3 finalTrajectoryCalculation;
    private Vector3 p;
    private Vector3 v;
    private float s;
    private Vector3 zero = Vector3.zero;
    [DoNotSerialize] public ControlInput inputTrigger;
    [DoNotSerialize] public ControlOutput outputTrigger;
    
    protected override void Definition()
    {
        directionToTarget = ValueInput<Vector3>("directionToTarget", Vector3.zero);
        targetForwardDirection = ValueInput<Vector3>("targetForwardDirection", Vector3.zero);
        targetSpeed = ValueInput<float>("targetSpeed", 0f);
        projectileSpeed = ValueInput<float>("projectileSpeed", 0f);

        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            p = flow.GetValue<Vector3>(directionToTarget);
            v = flow.GetValue<Vector3>(targetForwardDirection) * flow.GetValue<float>(targetSpeed);
            s = flow.GetValue<float>(projectileSpeed);
            float a = Vector3.Dot(v, v) - s * s;
            float b = Vector3.Dot(p, v);
            float c = Vector3.Dot(p, p);
            float d = b * b - a * c;
            if (d < .01f)
            {
                finalTrajectoryCalculation = Vector3.zero;
                return outputTrigger;
            }

            float sqrt = Mathf.Sqrt(d);
            float t1 = (-b - sqrt) / c;
            float t2 = (-b + sqrt) / c;

            float t = 0;
            if (t1 < 0 && t2 < 0) t = 0;
            else if (t1 < 0) t = t2;
            else if (t2 < 0) t = t1;
            else
            {
                t = Mathf.Max(new float[] {t1, t2});
            }

            finalTrajectoryCalculation = t * p + v;
            return outputTrigger;
        });
        
        outputTrigger = ControlOutput("outputTrigger");
        directionToPredictedPosition = ValueOutput<Vector3>("directionToPredictedPosition", (flow) => finalTrajectoryCalculation);
    }
}
