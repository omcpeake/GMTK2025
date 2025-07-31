using TMPro;
using UnityEngine;



public class LoopCounter : MonoBehaviour
{
    [SerializeField] int lastGateNum = 2;
    private int currentLoop = 0;
    private int currentGate = 0;
    private int nextExpectedGate = 0;
    private int previousGate = 99;

    private bool goingInReverse = false;
    private bool goingForwards = false;

    [SerializeField] private TextMeshProUGUI loopCountText;


    public void OnGateEnter(int gateNumber, Collider2D collision)
    {
        //The player has entered the next gate in the sequence
        previousGate = currentGate;
        currentGate = gateNumber;


        if (currentGate == nextExpectedGate) //player moving forwards
        {
            goingForwards = true;
            goingInReverse = false;

            if (currentGate == lastGateNum)
                nextExpectedGate = 0;
            else
                nextExpectedGate++;
        }
        else if(currentGate == previousGate)// player at the same gate
        {
            goingForwards = false;
            goingInReverse = false;
        }
        else // player moving backwards
        {
            goingForwards = false;
            goingInReverse = true;

        }

        //Decide if we need to increase or decrease the loop count
        if (gateNumber == 0)
        {
            if (goingForwards)
                currentLoop++;

           UpdateUI();
        }

        Debug.Log($"Player has entered gate {currentGate}. Next expected gate is {nextExpectedGate}.");

    }

    private void UpdateUI()
    {
        loopCountText.SetText(currentLoop.ToString());
    }

}
