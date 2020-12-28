using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using UnityEngine.UI;
using System.Globalization;

public class CardGenerator : MonoBehaviour
{
	//public Image tex;
	public Texture2D texxxx;
	private Camera myCamera;
	private bool takeScreenshotOnNextFrame;
	private string cardName;
	private Text text1;
    private Text text2;
	private string[] words = {"crema", "café", "estrella", "explosión", "guitarra", "plástico",
							"navaja", "martillo", "libros", "lápiz", "lapicera", "aluminio", "embarcación", 
							"letra", "agujeta", "ventana", "librería", "sonido", "universidad", "rueda", "perro", 
							"llaves", "camisa", "pelo", "papá", "sillón", "felicidad", "catre", "teclado", "servilleta", 
							"escuela", "pantalla", "sol", "codo", "tenedor", "estadística", "mapa", "agua", "mensaje", 
							"lima", "cohete", "rey", "edificio", "césped", "presidencia", "hojas", "parlante", "colegio", 
							"granizo", "pestaña", "lámpara", "mano", "monitor", "flor", "música", "hombre", "tornillo", 
							"habitación", "velero", "abuela", "abuelo", "palo", "satélite", "templo", "lentes", "bolígrafo", 
							"plato", "nube", "gobierno", "botella", "castillo", "enano", "casa", "libro", "persona", "planeta", 
							"televisor", "guantes", "metal", "teléfono", "proyector", "mono", "remera", "muela", "petróleo", 
							"percha", "remate", "debate", "anillo", "cuaderno", "ruido", "pared", "taladro", "herramienta",
							"cartas", "chocolate", "anteojos", "impresora", "caramelos", "living", "luces", "angustia",
							"zapato", "bomba", "lluvia", "ojo", "corbata", "periódico", "diente", "planta", "chupetín", 
							"buzo", "oficina", "persiana", "puerta", "tío", "silla", "ensalada", "pradera", "zoológico", 
							"candidato", "deporte", "recipiente", "diarios", "fotografía", "ave", "hierro", "refugio", 
							"pantalón", "barco", "carne", "nieve", "tecla", "humedad", "pistola", "departamento", "celular",
							"tristeza", "hipopótamo", "sofá", "cama", "árbol", "mesada", "campera", "discurso", "auto", 
							"cinturón", "rúcula", "famoso", "madera", "lentejas", "piso", "maletín", "reloj", "diputado", 
							"cuchillo", "desodorante", "candado", "luz", "montañas", "computadora", "radio", "moño", "cuadro", 
							"calor", "partido", "teatro", "bife", "fiesta", "bala", "auriculares"};

    // Start is called before the first frame update
    void Start(){
    	myCamera = gameObject.GetComponent<Camera>();
    	text1 = GameObject.Find("text1").GetComponent<Text>();
		text2 = GameObject.Find("text2").GetComponent<Text>();
		generateWords(Screen.width, Screen.height);
        
    }

    private void OnPostRender(){
    	if(takeScreenshotOnNextFrame){
    		Debug.Log(cardName);
    		takeScreenshotOnNextFrame = false;
    		RenderTexture renderTexture = myCamera.targetTexture;


    		Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
    		Rect rect = new Rect(0,0,renderTexture.width, renderTexture.height);
    		renderResult.ReadPixels(rect,0,0);
    		byte[] byteArray = renderResult.EncodeToPNG();
    		System.IO.File.WriteAllBytes(Application.dataPath + "/Cards/"+ cardName + ".jpg", byteArray);
    		
    		RenderTexture.ReleaseTemporary(renderTexture);
    		myCamera.targetTexture = null;
    	}
    }

    private void TakeScreenshot(int width, int height){
    	myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
    	takeScreenshotOnNextFrame = true;
    }

    public void generateWords(int width, int height){

    	foreach(string word in words){
    		//Debug.Log(word);
    		text1.text = word.ToUpper(new CultureInfo("es-ES", false));
    		text2.text = word.ToUpper(new CultureInfo("es-ES", false));
    		cardName = word;
    		myCamera.enabled = false;
    		myCamera.Render();
    		myCamera.enabled = true;
    		TakeScreenshot(width, height);
    	}

    	
    }

}
