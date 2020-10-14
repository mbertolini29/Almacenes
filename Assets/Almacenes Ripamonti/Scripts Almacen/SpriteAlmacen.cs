using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum enumItems
{
    caja = 0,
    cajaAplicada = 1,
    cajaAplicadaVarias = 2,
    camisaNaranja = 3,
    camisaRosada = 4,
    cinto = 5,
    copaMartini = 6,
    copaFina = 7,
    estanteria = 8,
    jarronAzul = 9,
    jarronNaranja = 10,
    jarronRosa = 11,
    jean = 12,
    luz = 13,
    mesa = 14,
    mueble = 15,
    vaso = 16,
    zapatosAm = 17,
    zapatosGrises = 18,
    zapatosVerdes = 19
}
public enum enumInterfaz
{
    InterfazSprite1 = 0,
    InterfazSprite2 = 1,
    InterfazSprite3 = 2,
    InterfazSprite4 = 3,
    InterfazSprite5 = 4,
    InterfazSprite6 = 5,
    InterfazSprite7 = 6,
    InterfazSprite8 = 7,
    InterfazSprite9 = 8,
    InterfazSprite10 = 9,
    InterfazSprite11 = 10,
    InterfazSprite12 = 11,
    InterfazSprite13 = 12,
    InterfazSprite14 = 13,
    InterfazSprite15 = 14,
    InterfazSprite16 = 15,
    InterfazSprite17 = 16
}
public enum enumEtapas
{ //no lo use todavia..
    EtapaComida = 0,
    EtapaRopa = 1,
    EtapaBebida = 2,
    EtapaFlores = 3,
    EtapaHerramientas = 4
}
public enum enumItemComida
{
    Banana = 0,
    FrascoAceituna = 1,
    FrascoAmarillo = 2,
    FrascoDulce = 3,
    FrascoMermelada = 4,
    HarinaMaiz = 5,
    HarinaTrigo = 6,
    Leche = 7,
    Maiz = 8,
    Manzana = 9,
    Pan = 10,
    QuesoChorizo = 11,
    QuesoFrasco = 12,
    QuesoJamon = 13,
    QuesoP = 14,
    Rabano = 15,
    Tomate = 16,
    Frasco = 17,
    Queso = 18
}
public enum enumItemRopa
{
    CamisaNaranja = 0,
    CamisaRosa = 1,
    GorroAmarillo = 2,
    GorroNegro = 3,
    Guantes = 4,
    PantalonAzul = 5,
    Pantaloncremita = 6,
    Reloj = 7,
    Remera = 8,
    Saco = 9,
    Vestido = 10,
    ZapatosHombre = 11,
    ZapatosMujer = 12,
    Gorro = 13,
    Camisa = 14,
    Pantalon = 15
}
public enum enumItemBebida
{
    Bidon = 0,
    Blanca2 = 1,
    Blanca5 = 2,
    CremaBordo = 3,
    CremaClara = 4,
    Marron = 5,
    Negra4 = 6,
    Negra6 = 7,
    Roja = 8,
    Verde = 9,
    XXX3 = 10,
    XXX8 = 11,
    Blanca = 12,
    Negra = 13,
    XXX = 14,
    Crema = 15
}
public enum enumItemFlor
{
    FloresAmarilla = 0,
    FloresNaranja = 1,
    FloresRosa = 2,
    FloresVerde = 3,
    FloresVioleta = 4,
    MacetaAmarilla = 5,
    MacetaRoja = 6,
    MacetaRosa = 7,
    MacetaVioleta = 8
}
public enum enumItemHerramienta
{
    Cuchillo = 0,
    Herramienta4 = 1,
    Herramienta5 = 2,
    Oz = 3,
    Pala = 4,
    Palo = 5,
    Rastrillo = 6,
    RepisaMartillo = 7,
    RepisaMasa = 8,
    RepisaPinza = 9,
    Ruedas = 10,
    Serrucho = 11,
    Repisa = 12,
    Herramienta = 13
}

public class SpriteAlmacen : MonoBehaviour
{
    [SerializeField] protected Sprite[] intefazSprite = null;
    [SerializeField] protected Sprite[] itemSpriteComida = null;
    [SerializeField] protected Sprite[] itemSpriteRopa = null;
    [SerializeField] protected Sprite[] itemSpriteBebida= null;
    [SerializeField] protected Sprite[] itemSpriteFlor= null;
    [SerializeField] protected Sprite[] itemSpriteHerramienta = null;

    //para elegir los objetos de las cajas.
    int cantSpriteComida = 17;
    int cantSpriteRopa = 13;
    int cantSpriteBebida = 12;
    int cantSpriteFlor = 9;
    int cantSpriteHerramienta = 12;

    //crear una class para las cajas...
    //nuevos
    public List<PedidoAlmacen> pedidosEtapaEnCurso = new List<PedidoAlmacen>(); //este tiene todas la info de la cajas...

    //creas la class que va a almacenar los pedidos...
    public PedidoAlmacen crearPedidosEtapa = new PedidoAlmacen(); //este se encarga de crearla..

    //Numeros aleatorios del 0 al 5
    List<int> numAleatorios = new List<int>(); //lista para los 5 numeros....

    //para mostrar las imagenes correspondientes en el pedido
    [SerializeField] GameObject[] pedidoImage = new GameObject[5]; //los 5 elementos que deben ir a la caja..
    [SerializeField] GameObject[] pedidoNum = new GameObject[5]; //los 5 elementos que deben ir a la caja..
    [SerializeField] GameObject[] imageCaja = new GameObject[5]; //los 5 elementos que deben ir a la caja..
    
    [SerializeField] AlmacenGameManager almacenGameManager;

    // Start is called before the first frame update
    void Start()
    {
        //agregar el script de itemAlmacen..
        almacenGameManager = GameObject.Find("GameManager").GetComponent<AlmacenGameManager>();
    
        //intefazSprite = Resources.LoadAll<Sprite>("almacen elementos");
        intefazSprite = Resources.LoadAll<Sprite>("Interfaz/elementos ui etapa 1");
        itemSpriteComida = Resources.LoadAll<Sprite>("Etapa 1 (comida)/SimbolosComida");
        itemSpriteRopa = Resources.LoadAll<Sprite>("Etapa 2 (ropa)/SimbolosRopa");
        itemSpriteBebida = Resources.LoadAll<Sprite>("Etapa 3 (bebidas)/SimbolosBebidas");
        itemSpriteFlor = Resources.LoadAll<Sprite>("Etapa 4 (flores)/SimbolosFlores");
        itemSpriteHerramienta = Resources.LoadAll<Sprite>("Etapa 5 (herramientas)/SimbolosHerramientas");

        NumeroAleatorios();

        RandomSprite();

        for (int i = 0; i < pedidoImage.Length; i++)
        {
            //busca los gameobject de interfazPedido
            pedidoImage[i] = GameObject.Find("PedidoImage" + i); //busca el gameobject que va a mover los objetos..
            pedidoNum[i] = GameObject.Find("PedidoNum" + i); //busca el gameobject que va a mover los objetos..

            //interfazCaja
            imageCaja[i] = GameObject.Find("ImageCaja" + i); //busca el gameobject que va a mover los objetos..
        }

        MostrarPedido();
    }
    void MostrarPedido()
    {
        for (int i = 0; i < 5; i++) // 5 pedidos...
        {
            Sprite sprite = null;

            switch (almacenGameManager.NumEtapa())
            {
                case 0:
                    sprite = GetSpriteComida(pedidosEtapaEnCurso[i].numSpriteCorrecto[0]);
                    break;
                case 1:
                    sprite = GetSpriteRopa(pedidosEtapaEnCurso[i].numSpriteCorrecto[0]);
                    break;
                case 2:
                    sprite = GetSpriteBebida(pedidosEtapaEnCurso[i].numSpriteCorrecto[0]);
                    break;
                case 3:
                    sprite = GetSpriteFlor(pedidosEtapaEnCurso[i].numSpriteCorrecto[0]);
                    break;
                case 4:
                    sprite = GetSpriteHerramienta(pedidosEtapaEnCurso[i].numSpriteCorrecto[0]);
                    break;
            }

            //interfazPedido
            pedidoImage[i].GetComponent<Image>().sprite = sprite;
            pedidoNum[i].GetComponent<Text>().text = "" + pedidosEtapaEnCurso[i].cantElementosCorrectos.ToString();            
            
            //interfazCaja
            imageCaja[numAleatorios[i]].GetComponent<Image>().sprite = sprite;
        }
    }
    void NumeroAleatorios()
    {
        //Numeros aleatorios para las cajas
        for (int i = 0; i < 5; i++)
        {
            //cantidad de numeros por caja
            int random = UnityEngine.Random.Range(0, 5);

            while (numAleatorios.Contains(random))
            {
                random = UnityEngine.Random.Range(0, 5);
            }
            numAleatorios.Add(random); //este para lograr que la lista sea unica,, sin numeros repetidos...
        }       
    }
    public void RandomSprite()
    {
        int cantSprite = 0;

        switch (almacenGameManager.NumEtapa())
        {
            case 0:
                cantSprite = cantSpriteComida;
                break;
            case 1:
                cantSprite = cantSpriteRopa;
                break;
            case 2:
                cantSprite = cantSpriteBebida;
                break;
            case 3:
                cantSprite = cantSpriteFlor;
                break;
            case 4:
                cantSprite = cantSpriteHerramienta;
                break;
        }

        List<int> numRandom = new List<int>(); //lista para los 5 numeros....

        //cantidad de cajas
        for (int i = 0; i < 5; i++)
        {
            crearPedidosEtapa = new PedidoAlmacen();
            crearPedidosEtapa._numCaja = numAleatorios[i]; //esto te va a decir el numero en el que va a aparecer en la caja...
            crearPedidosEtapa.cantElementosCorrectos = UnityEngine.Random.Range(1, 6); // para que elija la cantidad de elemntos que va en la caja del 1 al 5 

            //cantidad de numeros por caja
            int random = UnityEngine.Random.Range(0, cantSprite);

            while (numRandom.Contains(random))
            {
                random = UnityEngine.Random.Range(0, cantSprite);
            }
            numRandom.Add(random); //este para lograr que la lista sea unica,, sin numeros repetidos...

            crearPedidosEtapa.GuardarNumSprite(numRandom[i]); //esto lo guarda en el pedido

            pedidosEtapaEnCurso.Add(crearPedidosEtapa);
        }
    }
    //para obtener el sprite de un objeto de la interfaz del almacen...
    public Sprite GetInterfazSprite(int numSprite)
    {
        return intefazSprite[numSprite];
    }

    //para obtener el sprite Ropa del almacen...
    public Sprite GetSpriteComida(int numSprite)
    {
        return itemSpriteComida[numSprite];
    }
    //para obtener el sprite Ropa del almacen...
    public Sprite GetSpriteRopa(int numSprite)
    {
        return itemSpriteRopa[numSprite];
    }
    public Sprite GetSpriteBebida(int numSprite)
    {
        return itemSpriteBebida[numSprite];
    }
    public Sprite GetSpriteFlor(int numSprite)
    {
        return itemSpriteFlor[numSprite];
    }
    public Sprite GetSpriteHerramienta(int numSprite)
    {
        return itemSpriteHerramienta[numSprite];
    }
}