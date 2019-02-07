using UnityEngine;

public class BulletNumberDisplay : MonoBehaviour
{
    private BulletController bulletController;

    // Use this for initialization
    private void Start()
    {
        bulletController = transform.parent.gameObject.GetComponent<BulletController>();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}