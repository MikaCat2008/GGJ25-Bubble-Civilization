using UnityEngine;



//Control Camera Movement oin Exploration Mode
public class PlayerController : MonoBehaviour
{
    public float Speed = 10.0f;


    //zoom speed
    //zoom max





    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 PositionDelta = new Vector3(Speed * horizontal * Time.deltaTime, Speed * vertical * Time.deltaTime, 0);
        this.gameObject.transform.position += PositionDelta;
    }

}
