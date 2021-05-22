using UnityEngine;
public class Pointer : MonoBehaviour
{
    public Transform Target;
    public Transform Spirit;
    void Update()
    {

        Spirit.LookAt(Target);
        transform.rotation = Quaternion.Lerp(transform.rotation, Spirit.rotation, Time.deltaTime * 5f);
        
    }
}
