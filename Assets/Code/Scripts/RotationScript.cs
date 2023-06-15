using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Sensors.Reflection;
using UnityEngine;

public class RotationScript : Agent
{
    [Header("Specific to 3D Ball")]
    [SerializeReference] public GameObject BallObject;
    Rigidbody BallRigidBody;
    Vector3 BallPosition;
    EnvironmentParameters ResetParameters;

    public override void Initialize()
    {
        BallRigidBody = BallObject.GetComponent<Rigidbody>();
        ResetParameters = Academy.Instance.EnvironmentParameters;
        BallPosition = BallObject.transform.position;
        SetResetParameters();
    }

    [Observable(numStackedObservations: 9)]
    Vector2 Rotation
    {
        get
        {
            return new Vector2(gameObject.transform.rotation.z, gameObject.transform.rotation.x);
        }
    }

    [Observable(numStackedObservations: 9)]
    Vector3 PositionDelta
    {
        get
        {
            return BallObject.transform.position - gameObject.transform.position;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {/*
        sensor.AddObservation(gameObject.transform.position.x);
        sensor.AddObservation(gameObject.transform.position.z);
        sensor.AddObservation(gameObject.transform.position.y);
        sensor.AddObservation(BallObject.transform.position - gameObject.transform.position);
        sensor.AddObservation(BallRigidBody.velocity);*/
    }


    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var continuousActions = actionBuffers.ContinuousActions;
        var actionZ = 2f * Mathf.Clamp(continuousActions[0], -1f, 1f);
        var actionX = 2f * Mathf.Clamp(continuousActions[1], -1f, 1f);

        if ((gameObject.transform.rotation.z < 0.25f && actionZ > 0f) ||
            (gameObject.transform.rotation.z > -0.25f && actionZ < 0f))
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 1), actionZ);
        }

        if ((gameObject.transform.rotation.x < 0.25f && actionX > 0f) ||
            (gameObject.transform.rotation.x > -0.25f && actionX < 0f))
        {
            gameObject.transform.Rotate(new Vector3(1, 0, 0), actionX);
        }
        if ((BallObject.transform.position.y - gameObject.transform.position.y) < -2f ||
            Mathf.Abs(BallObject.transform.position.x - gameObject.transform.position.x) > 3f ||
            Mathf.Abs(BallObject.transform.position.z - gameObject.transform.position.z) > 3f)
        {
            SetReward(-1f);
            EndEpisode();
        }
        else
        {
            SetReward(0.1f);
        }


    }


    public override void OnEpisodeBegin()
    {
        gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        BallRigidBody.velocity = new Vector3(0f, 0f, 0f);
        BallObject.transform.position = BallPosition;
    }
    Transform mapsTransform;
    float rotationStrenght;
    public void SetResetParameters()
    {
        SetBall();
    }

    public void SetBall()
    {
        //Set the attributes of the ball by fetching the information from the academy
        BallRigidBody.mass = ResetParameters.GetWithDefault("mass", 1.0f);
        var scale = ResetParameters.GetWithDefault("scale", 1.0f);
        BallObject.transform.localScale = new Vector3(scale, scale, scale);

    }
}
