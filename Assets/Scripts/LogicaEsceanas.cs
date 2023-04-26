using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaEsceanas : MonoBehaviour
{

    [SerializeField] private GameObject Lose;

    [SerializeField] private GameObject Win;

    public int NumEscenaSig;

    // Start is called before the first frame update
    public void Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void Siguiente()
    {
        
        

        SceneManager.LoadScene(NumEscenaSig);
        

       
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
