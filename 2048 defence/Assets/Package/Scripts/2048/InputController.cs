using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemDir { None, Left, Right, Up, Down }

public class InputController : MonoBehaviour
{
    public ItemDir tempDir;
    public MainHolderController mainController;
    [SerializeField]
    private LevelInitManager levelInitManager;

    [Header("Grid Timer Settings")]
//  [SerializeField]
    public float TimeBetweenMovements = 10f;//TODO: Change this 
    [SerializeField]
    private bool gridMovementReady = true;

    // Use this for initialization
    private void Start()
    {
        mainController = transform.GetComponent<MainHolderController>();
        if(levelInitManager != null)
        {
            TimeBetweenMovements = levelInitManager.TimeBetweenGridMovements;
        }
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
        if (gridMovementReady)
        {
            if (Input.GetKeyDown("up"))
            {
                ActiveCountdownTimer();
                return ItemDir.Up;
            }
            if (Input.GetKeyDown("down"))
            {
                ActiveCountdownTimer();
                return ItemDir.Down;
            }
            if (Input.GetKeyDown("left"))
            {
                ActiveCountdownTimer();
                return ItemDir.Left;
            }
            if (Input.GetKeyDown("right"))
            {
                ActiveCountdownTimer();
                return ItemDir.Right;
            }
            return ItemDir.None;
        }
        else
        {
            return ItemDir.None;
        }
    }

    private void ActiveCountdownTimer()
    {
        //called to acitvate ienum timer, forces set amount of time wait between inputs to prevent spamming 
        gridMovementReady = false;
       // Debug.Log("GMR: " + gridMovementReady);
        StartCoroutine("MovementCountdownTimer", TimeBetweenMovements);


    }
    private IEnumerator MovementCountdownTimer(int time)
    {
        float timer = time;
        while (timer > 0)
        {
            yield return new WaitForSeconds(0.01f);
            timer -= 0.01f;
          
        }
        gridMovementReady = true;
       // Debug.Log("GMR: " + gridMovementReady);
    }
}