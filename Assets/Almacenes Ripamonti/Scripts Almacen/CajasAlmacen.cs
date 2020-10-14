using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class CajasAlmacen : MonoBehaviour
{
    Rigidbody2D rb;
    Camera cam;

    //Mercaderia
    [SerializeField] LayerMask layerMaskMercaderia; //agregar un Layer ---> Mercaderia
    [SerializeField] LayerMask layerMaskCajas; //agregar un Layer ---> Mercaderia

    [SerializeField] GameObject globoPensativo;
    [SerializeField] GameObject moverMercaderia;
    [SerializeField] GameObject numGlobo;

    [SerializeField] Sprite spriteAgarraste = null; //sprite del objeto que agarraste..
    string nomElemColisionado; //para que funcione el raycast
    [SerializeField] int numSpriteAgarraste = -1; //sprite del objeto que agarraste.. (lo pongo en negativo porque el cero es un numero del sprite!

    [SerializeField] int numCaja = 0;
          
    [SerializeField] bool tocaCaja = false; //si colisiono y puede agarrar..
    [SerializeField] bool tocaMueble = false; //si colisiono y puede agarrar..
    [SerializeField] bool tocaCesto = false; //si colisiono y tirar..
    [SerializeField] bool presionCesto = false; //si colisiono y tirar..
    [SerializeField] bool estaAgarrando = false; //si esta agarrando

    [SerializeField] SpriteAlmacen spriteAlmacen;
    [SerializeField] AlmacenGameManager almacenGameManager;

    [SerializeField] GameObject[] imageText = new GameObject[5]; //los 5 elementos que deben ir a la caja..

    [SerializeField] GameObject cantElementos; //los 5 elementos que deben ir a la caja..
    [SerializeField] Text numElementoGlobo;
    [SerializeField] int numElementos = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        //agregar el script de itemAlmacen..
        spriteAlmacen = GameObject.Find("GameManager").GetComponent<SpriteAlmacen>();
        almacenGameManager = GameObject.Find("GameManager").GetComponent<AlmacenGameManager>();

        globoPensativo = GameObject.Find("GloboPensativo"); //busca el gameobject con el globo....
        moverMercaderia = GameObject.Find("ObjetoPersonaje"); //busca el gameobject que va a mover los objetos..
        numGlobo = GameObject.Find("NumGlobo"); //busca el gameobject que va a mover los objetos..
        cantElementos = GameObject.Find("CantElementos"); //busca el gameobject que va a mover los objetos..
                
        for (int i = 0; i < imageText.Length; i++)
        {
            imageText[i] = GameObject.Find("ImageText" + i); //busca el gameobject que tiene el texto de numeros..
        }

        //globoPensativo.SetActive(false);
        //desactivas el globo..
        EstadoGlobo();
    }

    // Update is called once per frame
    void Update()
    {
        //primero llamas al rayo o disparo de la camara..
        Vector3 posSelection = cam.ScreenToWorldPoint(Input.mousePosition);

        if(estaAgarrando)
        {
            //si esta prendido Mostrar numero elemento en el globo 
            Vector3 numPosGlobo = cam.WorldToScreenPoint(numGlobo.transform.position);
            numElementoGlobo.transform.position = numPosGlobo;
        }

        //para parar el movimiento
        Vector2 speed = rb.velocity.normalized;
        bool isWalking = speed.sqrMagnitude > 0;
        if (!isWalking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //esto es para ver con que interactua..
                Vector2 mousePos2D = new Vector2(posSelection.x, posSelection.y);

                //para detectar si el rayo choca con algo, usaremos la clase de fisica..
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 25f, layerMaskMercaderia);

                if (hit.collider != null) //
                {
                    Debug.Log(hit.collider.gameObject.name);

                    Etapas(hit);

                    if (tocaMueble)
                    {
                        //if (hit.collider.gameObject.name == nomElemColisionado)
                        if ((spriteAgarraste != null) && (spriteAgarraste.name.Contains(nomElemColisionado)))
                        {
                            EstadoGlobo();
                            AgarrarObjeto();
                        }
                    }                    
                }

                RaycastHit2D hitCaja = Physics2D.Raycast(mousePos2D, Vector2.zero, 25f, layerMaskCajas);

                if (hitCaja.collider != null) //
                {
                    if ((estaAgarrando) && (tocaCaja))
                    {
                        AgregaObjetoCaja();
                    }
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (ColisionMueble(collision))
        {
            //Debug.Log("toca mueble");
            tocaMueble = true; //  

            //para que funcione el raycast!
            nomElemColisionado = collision.gameObject.name;
        }
        else if (collision.gameObject.name.Contains("CajaSola"))
        {
            tocaCaja = true;
            numCaja = int.Parse(collision.gameObject.name.Replace("CajaSola", ""));      
        }

        if(collision.gameObject.name == "CestoPos")
        {
            tocaCesto = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        tocaMueble = false;
        tocaCaja = false;
        tocaCesto = false;
    }
    public bool ColisionMueble (Collider2D _collision)
    {
        //esto lo podriamos crear en una funcion, para que dependa del nivel y asi poder usar el mismo codigo...
        var ignoreCase = true; //para ignorar mayusculas.. si esta activado, las toma igual.
        bool nombreObjectos = false;

        switch (almacenGameManager.NumEtapa())
        {
            case 0:
                nombreObjectos = enumItemComida.TryParse(_collision.gameObject.name, ignoreCase, out enumItemComida itemComida);
                break;
            case 1:
                nombreObjectos = enumItemRopa.TryParse(_collision.gameObject.name, ignoreCase, out enumItemRopa itemRopa);
                break;
            case 2:
                nombreObjectos = enumItemBebida.TryParse(_collision.gameObject.name, ignoreCase, out enumItemBebida itemBebida);
                break;
            case 3:
                nombreObjectos = enumItemFlor.TryParse(_collision.gameObject.name, ignoreCase, out enumItemFlor itemFlor);
                break;
            case 4:
                nombreObjectos = enumItemHerramienta.TryParse(_collision.gameObject.name, ignoreCase, out enumItemHerramienta itemHerramienta);
                break;
        }
        return nombreObjectos;
    }
    public void Etapas(RaycastHit2D _hit)
    { 
        //esto lo podriamos crear en una funcion, para que dependa del nivel y asi poder usar el mismo codigo...
        var ignoreCase = true; //para ignorar mayusculas.. si esta activado, las toma igual.
        bool nombreObjectos;

        switch (almacenGameManager.NumEtapa())
        {
            case 0:
                nombreObjectos = enumItemComida.TryParse(_hit.collider.gameObject.name, ignoreCase, out enumItemComida itemComida);
                if ((nombreObjectos) && (!estaAgarrando))
                {
                    spriteAgarraste = spriteAlmacen.GetSpriteComida((int)itemComida);
                    numSpriteAgarraste = (int)itemComida; //ahora guardamos el numero para comprar las listas mas facil..
                }
                break;
            case 1:
                nombreObjectos = enumItemRopa.TryParse(_hit.collider.gameObject.name, ignoreCase, out enumItemRopa itemRopa);
                if ((nombreObjectos) && (!estaAgarrando))
                {
                    //Debug.Log("toca mueble");
                    spriteAgarraste = spriteAlmacen.GetSpriteRopa((int)itemRopa);
                    numSpriteAgarraste = (int)itemRopa; //ahora guardamos el numero para comprar las listas mas facil..
                }
                break;
            case 2:
                nombreObjectos = enumItemBebida.TryParse(_hit.collider.gameObject.name, ignoreCase, out enumItemBebida itemBebida);
                if ((nombreObjectos) && (!estaAgarrando))
                {
                    //Debug.Log("toca mueble");
                    spriteAgarraste = spriteAlmacen.GetSpriteBebida((int)itemBebida);
                    numSpriteAgarraste = (int)itemBebida; //ahora guardamos el numero para comprar las listas mas facil..
                }
                break;
            case 3:
                nombreObjectos = enumItemFlor.TryParse(_hit.collider.gameObject.name, ignoreCase, out enumItemFlor itemFlor);
                if ((nombreObjectos) && (!estaAgarrando))
                {
                    //Debug.Log("toca mueble");
                    spriteAgarraste = spriteAlmacen.GetSpriteFlor((int)itemFlor);
                    numSpriteAgarraste = (int)itemFlor; //ahora guardamos el numero para comprar las listas mas facil..
                }
                break;
            case 4:
                nombreObjectos = enumItemHerramienta.TryParse(_hit.collider.gameObject.name, ignoreCase, out enumItemHerramienta itemHerramienta);
                if ((nombreObjectos) && (!estaAgarrando))
                {
                    //Debug.Log("toca mueble");
                    spriteAgarraste = spriteAlmacen.GetSpriteHerramienta((int)itemHerramienta);
                    numSpriteAgarraste = (int)itemHerramienta; //ahora guardamos el numero para comprar las listas mas facil..
                }
                break;
        }             
    }
    public void EstadoGlobo()
    {
        if ((globoPensativo.activeSelf)&&(!estaAgarrando))
        {
            globoPensativo.SetActive(false);
            cantElementos.SetActive(false);
        }
        else
        {
            globoPensativo.SetActive(true);
            cantElementos.SetActive(true);
            numGlobo = GameObject.Find("NumGlobo"); //busca el gameobject que va a mover los objetos..
            numElementoGlobo = GameObject.Find("CantElementos").GetComponent<Text>(); //busca el gameobject que va a mover los objetos..
        }
    }
    public void AgarrarObjeto()
    {
        //aca llama a una funcion donde dice bueno agarro tal sprite.
        moverMercaderia.GetComponent<SpriteRenderer>().sprite = spriteAgarraste;
        //si lo agarra
        estaAgarrando = true;

        AgregarElementoGlobo();
    }
    public void AgregarElementoGlobo()
    {
        //agrega uno elemento del globo..
        numElementos++;
        cantElementos.GetComponent<Text>().text = "" + numElementos.ToString();
    }
    public void RestarElementoGlobo()
    {
        //resta uno elemento del globo..
        numElementos--;
        cantElementos.GetComponent<Text>().text = "" + numElementos.ToString();

        if(numElementos == 0)
        {
            SoltarObjeto();
            EstadoGlobo();
        }
    }   
    public void SoltarObjeto()
    {
        //si estan en el mismo lugar otra cosas..
        moverMercaderia.GetComponent<SpriteRenderer>().sprite = null;
        //si no lo agarra
        estaAgarrando = false;
        spriteAgarraste = null;
        numSpriteAgarraste = -1;
    }
    public void EliminarElemento()
    {
        if((tocaCesto)&&(estaAgarrando))
        {
            //resta uno elemento del globo..
            numElementos--;
            cantElementos.GetComponent<Text>().text = "" + numElementos.ToString();

            //si tira es un error o no ???????????????                
            //almacenGameManager.ErroresCometidos();

            if (numElementos == 0)
            {
                SoltarObjeto();
                EstadoGlobo();
                numElementos = 0; //arreglo grocho
            }

            //controlar los errores...
            almacenGameManager.ErroresCometidos();
        }
        
        if ((tocaCaja)&&(presionCesto)) //me faltaria saber si un boton es presionado?
        {            
            //resta uno elemento de la caja..
            for (int i = 0; i < 5; i++)
            {
                //vos cuando chocas la caja 0 ---> en la clase, tenes que buscar cual elemento se encuentra en la caja 0
                if (numCaja == spriteAlmacen.pedidosEtapaEnCurso[i]._numCaja)
                {
                    spriteAlmacen.pedidosEtapaEnCurso[i].RestarElemEnCaja();

                    //controlar los errores...
                    almacenGameManager.ErroresCometidos();

                    //tengo que buscar el contador de la caja 
                    imageText[numCaja].GetComponent<Text>().text = "x" + spriteAlmacen.pedidosEtapaEnCurso[i].cantElementosPuestos.ToString();
                }
            }
            //para desactivar 
            presionCesto = false;   
        }
        else
        {
            presionCesto = false;
        }
    }
    public void PresionaCesto()
    {
        presionCesto = true;
    }
    //necesito uno de sacar...
    public void AgregaObjetoCaja()
    {
        for (int i = 0; i < 5; i++)
        {
            //vos cuando chocas la caja 0 ---> en la clase, tenes que buscar cual elemento se encuentra en la caja 0
            if (numCaja == spriteAlmacen.pedidosEtapaEnCurso[i]._numCaja)
            {
                if (spriteAlmacen.pedidosEtapaEnCurso[i].NumSpriteCajaCorrecto() == numSpriteAgarraste)
                {
                    RestarElementoGlobo();
                    spriteAlmacen.pedidosEtapaEnCurso[i].SumarElemEnCaja();

                    //tengo que buscar el contador de la caja 
                    imageText[numCaja].GetComponent<Text>().text = "x" + spriteAlmacen.pedidosEtapaEnCurso[i].cantElementosPuestos.ToString();

                    if(spriteAlmacen.pedidosEtapaEnCurso[i].cantElementosPuestos == spriteAlmacen.pedidosEtapaEnCurso[i].cantElementosCorrectos)
                    {
                        spriteAlmacen.pedidosEtapaEnCurso[i].cajaLlena = true;
                    }
                    else
                    {
                        spriteAlmacen.pedidosEtapaEnCurso[i].cajaLlena = false;                        
                    }
                }
                else //sino da un error
                {
                    RestarElementoGlobo();
                    almacenGameManager.ErroresCometidos();
                }
            }
        }
        ControlarPedido();
    }
    public void SacarObjetoCaja()
    {
        for (int i = 0; i < 5; i++)
        {
            //vos cuando chocas la caja 0 ---> en la clase, tenes que buscar cual elemento se encuentra en la caja 0
            if (numCaja == spriteAlmacen.pedidosEtapaEnCurso[i]._numCaja)
            {
                if (spriteAlmacen.pedidosEtapaEnCurso[i].NumSpriteCajaCorrecto() == numSpriteAgarraste)
                {
                    AgregarElementoGlobo();
                    spriteAlmacen.pedidosEtapaEnCurso[i].RestarElemEnCaja();

                    //tengo que buscar el contador de la caja 
                    imageText[numCaja].GetComponent<Text>().text = "x" + spriteAlmacen.pedidosEtapaEnCurso[i].cantElementosPuestos.ToString();
                }
            }
        }
    }
    public void ControlarPedido()
    {
        for (int i = 0; i < 5; i++)
        {
            //vos cuando chocas la caja 0 ---> en la clase, tenes que buscar cual elemento se encuentra en la caja 0
            if (numCaja == spriteAlmacen.pedidosEtapaEnCurso[i]._numCaja)
            {
                if (spriteAlmacen.pedidosEtapaEnCurso[i].cajaLlena)
                {
                    almacenGameManager.SumaEntregas();
                }
            }
        }
        //si entrega el pedido.. (si presiona el boton) y no todas las cajas estan llenas... pierde...         
    }    
}