using UnityEngine;

public class GetBloodBallCompo : MonoBehaviour
{
    public void UpBloodGetPercent()
    {
        GameManager.instance.GetBallPercent += 15;
    }
}
