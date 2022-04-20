using UnityEngine;

public class RandomizeRotationAndScale : MonoBehaviour
{    
    [SerializeField] private Vector3 rotationMins = new Vector3(0f, -180f, 0f);
    [SerializeField] private Vector3 rotationMax = new Vector3(0f, 180f, 0f);

    [SerializeField] private Vector3 scaleMins = new Vector3(0.9f, 0.9f, 0.9f);
    [SerializeField] private Vector3 scaleMaxs = new Vector3(1.1f, 1.1f, 1.1f);
    public void RandomizeTransform()
    {
        Transform thisT = GetComponent<Transform>();

        Vector3 scale = new Vector3(Random.Range(scaleMins.x, scaleMaxs.x), Random.Range(scaleMins.y, scaleMaxs.y), Random.Range(scaleMins.z, scaleMaxs.z));
        Vector3 rotation = new Vector3(Random.Range(rotationMins.x, rotationMax.x), Random.Range(rotationMins.y, rotationMax.y), Random.Range(rotationMins.z, rotationMax.z));

        thisT.localScale = scale;
        thisT.localEulerAngles = rotation;
    }
}