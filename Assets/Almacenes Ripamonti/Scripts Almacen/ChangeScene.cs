using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject canvas;
    public void CambiarEscena(string _escena)
    {
        print("Cambiando de escena" + _escena);
        SceneManager.LoadScene(_escena);
    }

    public void CambiarEscena(Escenas _escena)
    {
        print("Cambiando de escena - " + _escena);
        SceneManager.LoadScene((int)_escena);
    }

    public void CambiarTuneles()
    {
        CambiarEscena(Escenas.Tuneles);
    }

    public void Salir()
    {
        print("Salir");
        Application.Quit();
    }

    public void Opciones(GameObject _pausa)
    {
        _pausa.SetActive(true);
    }

    public void Reanudar(GameObject _reanudar)
    {
        _reanudar.SetActive(false);
    }
}
