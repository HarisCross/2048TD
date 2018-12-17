using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject targetMinion;
    private GameObject child;
    private Transform startPos;
    private MinionDetails targetMinionDetails;
    public float movementSpeed = 2f;
    public float bulletValue = 0f;
    public float timer;
    private Vector3 newSize;

    // Use this for initialization
    private void Start()
    {
        startPos = transform;
        child = transform.GetChild(0).transform.gameObject;
        AdjustSize();
    }

    // Update is called once per frame
    private void Update()
    {
        if (targetMinion != null)
        {
            if (targetMinionDetails == null) { AssignDetails(); }

            MoveBullet();
        }

        SelfDestruct();
    }

    private void AdjustSize()
    {
        //change childs scale using value given

        if (bulletValue < 10f)
        {
            newSize = new Vector3(1f, 1f, 1f);
        }
        else
        {
            if (bulletValue < 25f)
            {
                newSize = new Vector3(1.25f, 1.25f, 1f);
            }
            else
            {
                if (bulletValue < 50f)
                {
                    newSize = new Vector3(1.5f, 1.5f, 1f);
                }
                else
                {
                    if (bulletValue < 100f)
                    {
                        newSize = new Vector3(2f, 2f, 1f);
                    }
                    else
                    {
                        newSize = new Vector3(3.5f, 3.5f, 1f);
                    }
                }
            }
        }

        child.transform.localScale = newSize;
    }

    private void SelfDestruct()
    {
        //trigger if target dies thend destroy

        if (targetMinion == null) { Destroy(transform.gameObject); }
    }

    private void AssignDetails()
    {
        targetMinionDetails = targetMinion.GetComponent<MinionDetails>();
    }

    private void MoveBullet()
    {
        // print("moving bullet");
        timer += Time.deltaTime * movementSpeed;

        transform.position = Vector2.Lerp(startPos.position, targetMinion.transform.position, timer);
    }

    public void AddValues(GameObject target, float value)
    {
        //change speed using value, higher numbers go slower

        bulletValue = value;
        targetMinion = target;

        if (value >= 0 && value <= 8)
        {
            movementSpeed = 3f;
        }
        else
        {
            if (value >= 16 && value <= 64)
            {
                movementSpeed = 2f;
            }
            else
            {
                movementSpeed = 1f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //on hitting object
        //print("hit something");
        if (collision.transform.gameObject == targetMinion)
        {
            //  print("hit target");

            //   targetMinion.GetComponent<MinionDetails>().healthCurrent -= bulletValue;
            targetMinion.GetComponent<MinionDetails>().DoDamage(bulletValue);
            Destroy(this.transform.gameObject);
        }
    }
}