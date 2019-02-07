using TMPro;
using UnityEngine;

public class TUIController : MonoBehaviour
{
    public GameObject TextGO;
    private TextMeshPro textComp;
    private TowerController towCont;

    // Use this for initialization
    private void Start()
    {
        textComp = TextGO.GetComponent<TextMeshPro>();
        towCont = transform.GetComponent<TowerController>();
    }

    // Update is called once per frame
    private void Update()
    {
        CountCheck();
        KeepTextStraight();
    }

    private void CountCheck()
    {
        //if there is no ammo display that

        if (towCont.shotResources == 0)
        {
            textComp.text = "NO AMMO";
        }
        else
        {
            textComp.text = towCont.shotResources.ToString();
        }
    }

    private void KeepTextStraight()
    {
        Vector3 newRot = new Vector3(0, 0, transform.rotation.z);

        TextGO.transform.rotation = Quaternion.Euler(newRot.x, newRot.y, newRot.z);
    }
}