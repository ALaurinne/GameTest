using UnityEngine;

[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor;
    float length, startpos;
    public Camera parallaxCamera;

    public void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = (parallaxCamera.transform.position.x * (1 - parallaxFactor));
        float dist = (parallaxCamera.transform.position.x * parallaxFactor);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp >= startpos + length - 3) startpos += length;
        else if (temp <= startpos - length + 3) startpos -= length;

    }

}