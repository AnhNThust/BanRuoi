    using UnityEngine;

public class UpdateBehaviour : MonoBehaviour
{
    private float coolDown = 0;
    private bool IsCoolingDown => coolDown > 0;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFireBallIfNotCoolingDown();
        }
    }

    public void OnButtonClicked()
    {
        SpawnFireBallIfNotCoolingDown();
    }

    public void SpawnFireBallIfNotCoolingDown()
    {
        if (IsCoolingDown)
        {
            ReduceCoolDown();
        }
        else
        {
            ApplyCoolDown();
            SpawnFireBall();
        }
    }

    public void ReduceCoolDown()
    {
        
    }

    public void ApplyCoolDown()
    {
        
    }

    public void SpawnFireBall()
    {
        
    }
}
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
