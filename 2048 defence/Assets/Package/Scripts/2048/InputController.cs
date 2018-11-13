using UnityEngine;

public enum ItemDir { None, Left, Right, Up, Down }

public class InputController : MonoBehaviour
{
    public ItemDir tempDir;
    public MainHolderController mainController;

    // Use this for initialization
    private void Start()
    {
        mainController = transform.GetComponent<MainHolderController>();
    }

    private void Update()
    {
        if (mainController.AnimationOccuring == false)
        {
            tempDir = GetDir();
        }
        else
        {
            tempDir = ItemDir.None;
        }

        //if (tempDir != ItemDir.None)
        //{
        //    print(tempDir);
        //}
    }

    private ItemDir GetDir()
    {
        

        if (Input.GetKeyDown("up"))
        {
            return ItemDir.Up;

        }
        if (Input.GetKeyDown("down"))
        {
            return ItemDir.Down;

        }
        if (Input.GetKeyDown("left"))
        {
          //  print("pressing left");
            return ItemDir.Left;

        }
        if (Input.GetKeyDown("right"))
        {
            return ItemDir.Right;

        }
        return ItemDir.None;
    }
}