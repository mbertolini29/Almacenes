using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int banderaAlmacenes, banderaLanzallamas, banderaTrenes, banderaTuneles;
    public string pj;
    public int puntos;
    public float posicionX, posicionY;
    public float mPosicionX, mPosicionY;

    void Start()
    {

    }

    /*
    
        El script de guardado está pensado para ser llamado desde las etapas, una vez terminada/completada,
        pasando su nombre en la función ganoEtapa(), por ejemplo:

            ganoEtapa(tuneles);

        Lo que va a hacer esto, es asignarle el valor 1 (verdadero) a la variable con el nombre de la etapa, 
        y a si mismo darle el valor 1 (verdadero) a la respectiva bandera
        de la etapa, la cual va a ser utilizada para la lógica ingame.

        Para un correcto funcionamiento del puntaje, cada etapa deberá incorporar algun tipo de bandera y limitador,
        para que en la misma partida no puedas ganar 4 veces el mismo minijuego
        y sumar 4 puntos para completar el juego.
    */

    public void GanoEtapa(string etapa){
        if(etapa=="almacenes"){
            banderaAlmacenes = 1;
        }
        if(etapa=="lanzallamas"){
            banderaLanzallamas = 1;
        }
        if(etapa=="trenes"){
            banderaTrenes = 1;
        }
        if(etapa=="tuneles"){
            banderaTuneles = 1;
        }

        /* El valor que se envia junto con la llamada de la funcion, es lo que se va a guardar como nombre de la
         variable. Teniendo en cuenta que no hay booleanos en PlayerPrefs usamos 1 para true y 0 para false */
        PlayerPrefs.GetInt(etapa, 1);
        //sumamos la perla a la variable puntos
        puntos++;
        guardarDatos();
    }

    //Masculino M y Femenino F
    public void guardarSexo(string aux){
    PlayerPrefs.SetString("sexo", aux);
    guardarDatos();
    }

    public void getSexo(){
        pj = PlayerPrefs.GetString("sexo");
    }


    // GUARDADO DE POSICIONES
    public void guardarPosicion(float x, float y){
        PlayerPrefs.SetFloat("posX", x);
        PlayerPrefs.SetFloat("posY", y);
    }

    public void obtenerPosicion(){
        posicionX = PlayerPrefs.GetFloat("posX");
        posicionY = PlayerPrefs.GetFloat("posY");
    }

    public void guardarPosicionMuseo(float x, float y){
        PlayerPrefs.SetFloat("mPosX", x);
        PlayerPrefs.SetFloat("mPosY", y);
    }

    public void obtenerPosicionMuseo(){
        mPosicionX = PlayerPrefs.GetFloat("mPosX");
        mPosicionY = PlayerPrefs.GetFloat("mPosY");
    }


    
    
    public void reanudarPartida(){
        banderaAlmacenes = PlayerPrefs.GetInt("almacenes");
        banderaLanzallamas = PlayerPrefs.GetInt("lanzallamas");
        banderaTrenes = PlayerPrefs.GetInt("trenes");
        banderaTuneles = PlayerPrefs.GetInt("tuneles");
        puntos = PlayerPrefs.GetInt("puntos");
    }

    public void nuevaPartida(){
        PlayerPrefs.SetInt("almacenes", 0);
        PlayerPrefs.SetInt("lanzallamas", 0);
        PlayerPrefs.SetInt("trenes", 0);
        PlayerPrefs.SetInt("tuneles", 0);
        PlayerPrefs.SetInt("puntos", 0);
        banderaAlmacenes = PlayerPrefs.GetInt("almacenes");
        banderaLanzallamas = PlayerPrefs.GetInt("lanzallamas");
        banderaTrenes = PlayerPrefs.GetInt("trenes");
        banderaTuneles = PlayerPrefs.GetInt("tuneles");
        puntos = PlayerPrefs.GetInt("puntos");
        guardarDatos();
    }

    void guardarDatos(){
        
        PlayerPrefs.Save();
        
    }



}