using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPredictor : MonoBehaviour
{

    public float initialVelocity = 200f;
    public Rigidbody2D rbdy;
    [SerializeField]
    Transform target;

    void Start()
	{
		CalculateVelocity(transform.position, target.position, 0);
	}

    Quaternion quaterion = Quaternion.AngleAxis(90, Vector3.forward);
	private Vector3 CalculateVelocity(Vector3 position, Vector3 target, float angle)
	{
        //FROM HERE: https://forum.unity.com/threads/how-to-calculate-force-needed-to-jump-towards-target-point.372288/
        //Vector3 p = target.position;

        float gravity = Physics2D.gravity.magnitude;
		// Selected angle in radians
		//float angle = initialAngle * Mathf.Deg2Rad;

		// Positions of this object and the target on the same plane
		Vector3 planarTarget = new Vector3(target.x, 0, 0);
		Vector3 planarPostion = new Vector3(position.x, 0, 0);

		// Planar distance between objects
		float distance = Vector3.Distance(planarTarget, planarPostion);
        // Distance along the y axis between objects
        //float yOffset = transform.position.y - p.y;
        var yOffset = position.y - target.y;

		float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

		Vector3 velocity = new Vector3(initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle), 0);

        // Rotate our velocity to match the direction between the two objects
        //float angleBetweenObjects = Vector3.Angle(Vector3.up, planarTarget - planarPostion);
        Vector3 finalVelocity = quaterion * velocity;
		if (target.x > position.x)
		{
            return Vector3.Reflect(finalVelocity, Vector3.right);
		}
        return finalVelocity;
		//// Fire!
		//rigid.velocity = finalVelocity;
		//// Alternative way:
		//// rigid.AddForce(finalVelocity * rigid.mass, ForceMode.Impulse);
	}

	// Update is called once per frame
	void Update()
    {
		var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 dir = new Vector3(1, 2, 0).normalized;
        var rad = Mathf.Atan2(dir.y, dir.x);
		Debug.DrawRay(transform.position, dir);
        //Debug.DrawLine(this.transform.position, worldPos);
        //var delta = Vector3.Normalize(worldPos - this.transform.position);

		if (Input.GetKeyDown(KeyCode.V))
		{
            var vel = CalculateVelocity(this.transform.position, target.position, rad);
			if (PredictRigidBodyLandPos(rbdy, vel, out var result))
			{
                Debug.Log($"Found result {result}");
			}
			else
			{
                Debug.Log("Found no result");
			}
		}
    }
    public struct PredictResult
    {
        public Vector3 position;
        public float rotation;
        public float landingTime;
    }
    bool PredictRigidBodyLandPos(Rigidbody2D sourceRigidbody, Vector3 velocity, out PredictResult result)
    {
        //const float landingYPoint = 0.15f;
        const float landingYPoint = -7f;

        //Disable Physics AutoSimulation
        Physics2D.simulationMode = SimulationMode2D.Script;

        //Shoot the Bullet 
        sourceRigidbody.velocity = velocity;

        //Get current Position and rotation
        Vector3 defaultPos = sourceRigidbody.position;
        var defaultRot = sourceRigidbody.transform.rotation;

        //Debug.Log("Predicting Future Pos from::: x " + defaultPos.x + " y:"
        //    + defaultPos.y + " z:" + defaultPos.z);

        //Exit after x seconds(In physics time) if Object does not land
        float timeOutTime = 15f;
        //The landing time that will be returned
        float landingTime = 0;

        //Determines if we landed successfully or not
        bool landedSuccess = false;

        //Simulate where it will be in x seconds
        while (timeOutTime >= Time.fixedDeltaTime)
        {
            timeOutTime -= Time.fixedDeltaTime;
            landingTime += Time.fixedDeltaTime;

            var lastPost = sourceRigidbody.position;
            Physics2D.Simulate(Time.fixedDeltaTime);

            Vector3 pos = sourceRigidbody.position;
            Debug.DrawLine(lastPost, pos, Color.red, 2f);
            //Debug.Log("Pos: " + pos.x + " " + pos.y + " " + pos.z);

            //Check if we have landed then break out of the loop
            if (pos.y < landingYPoint || Mathf.Approximately(pos.y, landingYPoint))
            {
                landedSuccess = true;
                Debug.LogWarning("Landed");
                break;
            }
        }

        //Get future position and rotation and save them to output
        Vector3 futurePos = sourceRigidbody.position;
        var futureRot = sourceRigidbody.rotation;

        result = new PredictResult();
        result.position = futurePos;
        result.rotation = futureRot;
        result.landingTime = landingTime;

        //Re-enable Physics AutoSimulation and Reset position and rotation
        Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
        sourceRigidbody.velocity = Vector3.zero;
        //sourceRigidbody.useGravity = false;

        sourceRigidbody.transform.position = defaultPos;
        sourceRigidbody.transform.rotation = defaultRot;

        return landedSuccess;
    }
}
