using UnityEngine;

public class TileMoving : MonoBehaviour
{
    private Vector3 startPos;
    float repeatedcenter;
    public float speed;
    public float ratio;
    private void Start()
    {
        startPos = transform.position;
        repeatedcenter = GetComponent<BoxCollider>().size.z / ratio;
    }
    private void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);

        if (transform.position.z < startPos.z- repeatedcenter)
        {
            transform.position = startPos;  
        }
    }
}
