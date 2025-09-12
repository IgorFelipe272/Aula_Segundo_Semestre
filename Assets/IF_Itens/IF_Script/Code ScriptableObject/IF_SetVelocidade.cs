using UnityEngine;

public class IF_SetVelocidade : MonoBehaviour
{

    public IF_VelocidadeManager manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(manager == null)
        {
            Debug.LogWarning("Tem q add o controle de velocidade");
        }
    }

}
