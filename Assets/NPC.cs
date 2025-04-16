using UnityEngine;

public class NPC : MonoBehaviour
{
    private float moveTimer = 0;
    private float moveWait;
    private Vector3 direction;
    public House house;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveWait = Random.Range(1f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        // in-progress random movement
        //moveTimer += Time.deltaTime;
        //if (moveTimer > moveWait)
        //{
        //    moveTimer = 0;
        //    Vector3[] movements = { new Vector3(0,0,0), new Vector3(0,1,0), 
        //        new Vector3 (1,0,0), new Vector3 (0,-1,0), new Vector3 (-1,0,0) };
        //    int index = Random.Range(0, movements.Length);
        //    direction = movements[index];
        //}
        //MoveCharacter();
    }

    public bool getHouseState()
    {
        return house.getNeedsFixing();
    }

    void MoveCharacter()
    {
        transform.position += (Vector3)(direction * 0.6f * Time.deltaTime);
    }
}
