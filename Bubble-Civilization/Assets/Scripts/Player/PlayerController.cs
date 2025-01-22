using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.right, -20);

    // Update is called once per frame
    void Update()
    {
        screenPosition = Input.mousePosition;
        
        //Get Ray and plane intersection point
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        transform.position = worldPosition;
    }



    public void RaycastWith3dColliders()
    {
        screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            worldPosition = hitData.point;
        }

        transform.position = worldPosition;
    }
    public void movePlayerWithMouse()
    {
        screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane + 1;

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = worldPosition;
    }
}
