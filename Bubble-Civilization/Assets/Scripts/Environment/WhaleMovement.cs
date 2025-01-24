using Unity.Mathematics;
using UnityEngine;

public class WhaleMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotationSpeed = 1.0f;
    public float rotationAngleCorectness = 90f;
    [SerializeField] LineRenderer movementPath;
    Vector3[] movementPositions;
    

    private bool startMovememnt = true;
    private int movememntIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetMovementPositions();
        SetOnPathStart();
        movementPath.enabled = false;
    }

    private void SetMovementPositions()
    {
        movementPositions = new Vector3[movementPath.positionCount];
        movementPath.GetPositions(movementPositions);
        for (var i = 0; i < movementPositions.Length; i++)
        {
            movementPositions[i] = movementPath.transform.TransformPoint(movementPositions[i]);
        }
        movememntIndex = 0;
        startMovememnt = true;

    }

    private void SetOnPathStart()
    {
        //transform.position = movementPath.transform.position;
        transform.position = movementPositions[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (startMovememnt)
        {
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = movementPositions[movememntIndex];
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

            //rotate
            Vector2 direction = targetPosition - currentPosition;
            float rotAngle = Mathf.Atan2(direction.normalized.x, direction.normalized.y);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, rotAngle * Mathf.Rad2Deg+rotationAngleCorectness), rotationSpeed);


            float distance = Vector2.Distance(currentPosition, targetPosition);
            if (distance <= 0.05f)
            {
                movememntIndex++;
            }

            if (movememntIndex > movementPositions.Length-1)
            {
                startMovememnt = false;
            }
        }
        
    }
}

