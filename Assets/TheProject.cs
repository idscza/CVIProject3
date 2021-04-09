using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheProject : MonoBehaviour
{
	public GameObject rojoalfa;
	public GameObject azulalfa;
	public GameObject manoroja;
	public GameObject manoazul;
	public GameObject cabezaroja;
	public GameObject sangre;
	public int phase = 0;
	int state = 0;
	public float elapsedTime;
    GameObject luzroja;
	GameObject luzazul;
	bool alive = true;
	float vidazul = 100f;
	float vidaroja = 100f;
	public GameObject fan1;
	public GameObject fan2;
	public GameObject fan3;
	public GameObject letr;
	
	// Start is called before the first frame update
    void Start()
    {
		GameObject piso = create_soil();
		Transform cam = Camera.main.GetComponent<Transform>();
		cam.position = new Vector3(6f,2.7f,0);
		cam.transform.rotation = Quaternion.Euler(15f,-90f,0);
		luces();
		letrero();
		pipol();
        fighters();
		blood ();
    }
	
	GameObject create_soil()
	{
		GameObject plane  = GameObject.CreatePrimitive(PrimitiveType.Plane);
		plane.transform.localScale = new Vector3 (4, 1, 4);
		var planeRenderer = plane.GetComponent<Renderer>();
		Material areMat = Resources.Load("Materials/arena", typeof(Material)) as Material;
		planeRenderer.material = areMat;
		
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.localScale = new Vector3 (10.5f, 0.1f, 10.5f);
		var cubeRenderer = cube.GetComponent<Renderer>();
		cubeRenderer.material.SetColor("_Color", Color.gray);
		Texture2D horm = (Texture2D) Resources.Load ("hormigon");
		cubeRenderer.material.mainTexture = horm;
		cube.transform.parent = plane.transform;
		
		wangcreator(cube);
		
		GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube2.transform.localScale = new Vector3 (10f, 0.12f, 0.5f);
		var cube2Renderer = cube2.GetComponent<Renderer>();
		cube2Renderer.material.SetColor("_Color", Color.gray);
		cube2Renderer.material.mainTexture = horm;
		cube2.transform.parent = cube.transform;
		
		GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		cylinder.transform.localScale = new Vector3 (2, 0.065f, 2);
		var cylinderRenderer = cylinder.GetComponent<Renderer>();
		cylinderRenderer.material.SetColor("_Color", Color.gray);
		cylinderRenderer.material.mainTexture = horm;
		cylinder.transform.parent = cube.transform;
		
		towers(plane);
		
		return plane;
	}
	
	void wangcreator(GameObject par){
	
		int[,] map = new int[10, 10];
		
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				GameObject cubew = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cubew.transform.localScale = new Vector3 (1,0.11f, 1);
				cubew.transform.position = new Vector3((-1*j)+4.5f, 0, i-4.5f);
				var cubewRenderer = cubew.GetComponent<Renderer>();
				string tile = generate_tile(i,j,map);
				Texture2D wangtile = (Texture2D) Resources.Load (tile);
				cubewRenderer.material.mainTexture = wangtile;
				cubew.transform.parent = par.transform;
			}
		}
	
	}
	
	
	Vector3 create_cords()
	{
		float elz = Random.Range(-45.0f, 45.0f);
		float elx = Random.Range(-45.0f, 45.0f);
		Vector3 coords = new Vector3 (elx, 0, elz);
		while (elz*elx < 49 && elz*elx > -49)
		{
			elz = Random.Range(-45.0f, 45.0f);
			elx = Random.Range(-45.0f, 45.0f);
			coords = new Vector3 (elx, 0, elz);
		}			
		return coords;
	}
	
	void pipol(){
		
		int xstart = -6;
		int zstart = 5;
		int onde = 1;
		bool done = false;
		
		while(!done){
			if (zstart == 5){
				fan1 = create_person(new Vector3(xstart,0,zstart), onde, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f),0,null);
				fan1.gameObject.AddComponent<Animator>();
				Animator anim = fan1.GetComponent<Animator>();
				anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Capsule");
			}else if (zstart == 4){
				fan2 = create_person(new Vector3(xstart,0,zstart), onde, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f),0,null);
				fan2.gameObject.AddComponent<Animator>();
				Animator anim = fan2.GetComponent<Animator>();
				anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Capsule1");
			}else if (zstart == 3){
				fan3 = create_person(new Vector3(xstart,0,zstart), onde, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f),0,null);
				fan3.gameObject.AddComponent<Animator>();
				Animator anim = fan3.GetComponent<Animator>();
				anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Capsule2");
			}else{
				int papito = (int)(Random.Range(0f, 3f));
				GameObject padre = null;
				if (papito == 0){
					padre = fan1;
				}else if (papito == 1){
					padre = fan2;
				}else if (papito == 2){
					padre = fan3;
				}
				create_person(new Vector3(xstart,0,zstart), onde, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f),0,padre);
			}
			
			zstart = zstart-1;
			if(zstart < -5){
				done = true;
				xstart = xstart+1;
				onde = 0;
			}
		}
		
		done = false;
		
		while(!done){
			int papito = (int)(Random.Range(0f, 3f));
				GameObject padre = null;
				if (papito == 0){
					padre = fan1;
				}else if (papito == 1){
					padre = fan2;
				}else if (papito == 2){
					padre = fan3;
				}
			create_person(new Vector3(xstart,0,zstart), onde, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f),3, padre);
			xstart = xstart+1;
			

			if(xstart > 5){
				done = true;
				zstart = zstart+1;
				onde = 1;
			}
		}
		
		done = false;
		
		while(!done){
			create_person(new Vector3(xstart,0,zstart), onde, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f),2, null);
			zstart = zstart+1;
			if(zstart > 5){
				done = true;
				xstart = xstart-1;
				onde = 0;
			}
		}
		
		done = false;
		
		while(!done){
			int papito = (int)(Random.Range(0f, 3f));
				GameObject padre = null;
				if (papito == 0){
					padre = fan1;
				}else if (papito == 1){
					padre = fan2;
				}else if (papito == 2){
					padre = fan3;
				}
			create_person(new Vector3(xstart,0,zstart), onde, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f),1,padre);
			xstart = xstart-1;
			if(xstart < -5){
				done = true;
			}
		}
	}
	
	GameObject create_person(Vector3 pos, int ori, Color ropita, int face, GameObject parent)
	{
		
		Vector3 legsize = new Vector3(0.25f, 0.5f, 0.25f);
		
		GameObject tronco = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		tronco.transform.localScale = new Vector3(0.6f, 0.75f, 0.6f);
		tronco.transform.position = new Vector3(pos.x, pos.y+1.4f, pos.z);
		var elRenderer3 = tronco.GetComponent<Renderer>();
		Texture2D tela = (Texture2D) Resources.Load ("tela");
		elRenderer3.material.mainTexture = tela;
		elRenderer3.material.SetColor("_Color", ropita);
		if (parent != null){
			tronco.transform.parent = parent.transform;
		}
		if (ori == 1)
		{
			GameObject pierna1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			pierna1.transform.localScale = legsize;
			pierna1.transform.position = new Vector3(pos.x, pos.y+0.5f, pos.z+0.17f);
			var elRenderer1 = pierna1.GetComponent<Renderer>();
			elRenderer1.material.SetColor("_Color", ropita);
			elRenderer1.material.mainTexture = tela;
			pierna1.transform.parent = tronco.transform;
			
			GameObject pierna2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			pierna2.transform.localScale = legsize;
			pierna2.transform.position = new Vector3(pos.x, pos.y+0.5f, pos.z-0.17f);
			var elRenderer2 = pierna2.GetComponent<Renderer>();
			elRenderer2.material.SetColor("_Color", ropita);
			elRenderer2.material.mainTexture = tela;
			pierna2.transform.parent = tronco.transform;
			
		}else{
			GameObject pierna1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			pierna1.transform.localScale = legsize;
			pierna1.transform.position = new Vector3(pos.x+0.17f, pos.y+0.5f, pos.z);
			var elRenderer1 = pierna1.GetComponent<Renderer>();
			elRenderer1.material.SetColor("_Color", ropita);
			elRenderer1.material.mainTexture = tela;
			pierna1.transform.parent = tronco.transform;
			
			GameObject pierna2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			pierna2.transform.localScale = legsize;
			pierna2.transform.position = new Vector3(pos.x-0.17f, pos.y+0.5f, pos.z);
			var elRenderer2 = pierna2.GetComponent<Renderer>();
			elRenderer2.material.SetColor("_Color", ropita);
			elRenderer2.material.mainTexture = tela;
			pierna2.transform.parent = tronco.transform;
		}
		
		GameObject cabeza = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		cabeza.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
		cabeza.transform.position = new Vector3(pos.x, pos.y+2.3f, pos.z);
		var elRenderer4 = cabeza.GetComponent<Renderer>();
		Material cara = Resources.Load("Materials/angry", typeof(Material)) as Material;
		elRenderer4.material = cara;
		var mirar = Quaternion.Euler(0,90f*face,0);
		cabeza.transform.rotation = mirar;
		cabeza.transform.parent = tronco.transform;
		cabezaroja = cabeza;
		
		return tronco;
		
	}
	
	void fighters(){
		
		Vector3 guerreroazul = new Vector3(0,0,0.8f);
		Vector3 guerrerorojo = new Vector3(0,0,-0.8f);		
		
		azulalfa = create_person(guerreroazul, 0, new Color(0f, 0.219f, 0.576f, 1f), 1,null);
		rojoalfa = create_person(guerrerorojo, 0, new Color(0.807f, 0.066f, 0.148f, 1f), 3,null);
		
		create_armor(guerreroazul,0, new Color(0f, 0.219f, 0.576f, 1f));
		
		create_armor(guerrerorojo,1, new Color(0.807f, 0.066f, 0.148f, 1f));
		
		
	}
	
	void create_armor(Vector3 pos, int ori, Color ropita){
	
		Vector3 ar1pos = new Vector3(pos.x+0.3f, pos.y+1.75f, pos.z-0.2f);
		var ar12rot = Quaternion.Euler(0,-45f,0);
		Vector3 ar2pos = new Vector3(pos.x+0.3f, pos.y+1.75f, pos.z-0.21f);
		Vector3 ar3pos = new Vector3(pos.x+0.3f, pos.y+1.465f, pos.z-0.2f);
		var ar34rot = Quaternion.Euler(0,-45f,-45f);
		Vector3 ar4pos = new Vector3(pos.x+0.3f, pos.y+1.465f, pos.z-0.21f);
			
		Vector3 manopos = new Vector3(pos.x-0.3f, pos.y+1.465f, pos.z);
		Vector3 espapos = new Vector3(pos.x-0.31f, pos.y+1.87f, pos.z-0.4f);
		var esparot = Quaternion.Euler(-45,0,0);

		
		if (ori != 0){
			
			ar1pos = new Vector3(pos.x-0.3f, pos.y+1.75f, pos.z+0.2f);
			ar12rot = Quaternion.Euler(0,-45f,0);
			ar2pos = new Vector3(pos.x-0.3f, pos.y+1.75f, pos.z+0.21f);
			ar3pos = new Vector3(pos.x-0.3f, pos.y+1.465f, pos.z+0.2f);
			ar34rot = Quaternion.Euler(0,-45f,-45f);
			ar4pos = new Vector3(pos.x-0.3f, pos.y+1.465f, pos.z+0.21f);
			
			manopos = new Vector3(pos.x+0.3f, pos.y+1.465f, pos.z);
			espapos = new Vector3(pos.x+0.31f, pos.y+1.87f, pos.z+0.4f);
			esparot = Quaternion.Euler(45,0,0);
			
		}
		
		GameObject armazon1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        armazon1.transform.position = ar1pos;
		armazon1.transform.localScale = new Vector3 (0.6f, 0.6f, 0.01f);
		armazon1.transform.rotation = ar12rot;
		var armazon1Renderer3 = armazon1.GetComponent<Renderer>();
		Material metMat = Resources.Load("Materials/metal", typeof(Material)) as Material;
		armazon1Renderer3.material = metMat;
		
		GameObject armazon2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        armazon2.transform.position = ar2pos;
		armazon2.transform.localScale = new Vector3 (0.55f, 0.55f, 0.01f);
		armazon2.transform.rotation = ar12rot;
		var armazon2Renderer3 = armazon2.GetComponent<Renderer>();
		Texture2D mad = (Texture2D) Resources.Load ("madera");
		armazon2Renderer3.material.SetColor("_Color",ropita);
		armazon2Renderer3.material.mainTexture = mad;
		armazon2.transform.parent = armazon1.transform;
		
		GameObject armazon3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        armazon3.transform.position = ar3pos;
		armazon3.transform.localScale = new Vector3 (0.425f, 0.425f, 0.01f);
		armazon3.transform.rotation = ar34rot;
		var armazon3Renderer3 = armazon3.GetComponent<Renderer>();
		armazon3Renderer3.material = metMat;
		armazon3.transform.parent = armazon1.transform;
		
		GameObject armazon4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        armazon4.transform.position = ar4pos;
		armazon4.transform.localScale = new Vector3 (0.375f, 0.375f, 0.01f);
		armazon4.transform.rotation = ar34rot;
		var armazon4Renderer3 = armazon4.GetComponent<Renderer>();
		armazon4Renderer3.material.mainTexture = mad;
		armazon4Renderer3.material.SetColor("_Color",ropita);
		armazon4.transform.parent = armazon1.transform;
		
		GameObject mano = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		mano.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
		mano.transform.position = manopos;
		var manoRenderer4 = mano.GetComponent<Renderer>();
		Texture2D tela = (Texture2D) Resources.Load ("tela");
		manoRenderer4.material.mainTexture = tela;
		manoRenderer4.material.SetColor("_Color", ropita);
		
		GameObject espada = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		espada.transform.localScale = new Vector3(0.04f, 0.7f, 0.08f);
		espada.transform.position = espapos;
		espada.transform.rotation = esparot;
		var espadaRenderer2 = espada.GetComponent<Renderer>();
		espadaRenderer2.material = metMat;
		espada.transform.parent = mano.transform;
	
		if (ori == 0){
			mano.transform.parent = azulalfa.transform;
			armazon1.transform.parent = azulalfa.transform;
			manoazul = mano;
			manoazul.gameObject.AddComponent<Animator>();
			Animator anim = manoazul.GetComponent<Animator>();
			anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("BlueHit");
		}
		else{
			mano.transform.parent = rojoalfa.transform;
			armazon1.transform.parent = rojoalfa.transform;
			manoroja = mano;
			manoroja.gameObject.AddComponent<Animator>();
			Animator anim = manoroja.GetComponent<Animator>();
			anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("RedHit");
		}
	}
	
	void blood(){
		GameObject blod = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		blod.transform.localScale = new Vector3 (0.1f, 0.10f, 0.1f);
		var blod1Renderer = blod.GetComponent<Renderer>();
		Color blodColor = new Color(0.4f, 0f, 0f, 0.7f);
		blod.transform.position = new Vector3 (0f, -1f, -2f);
		blod1Renderer.material.SetColor("_Color",blodColor);
		sangre = blod;
	}
	
	void towers(GameObject par){
		
		for (int i = -1; i < 2; i+=2){
			for (int j = -1; j < 2; j+=2){
				
				GameObject tow = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
				tow.transform.localScale = new Vector3 (5f, 4.5f, 5f);
				var towRenderer = tow.GetComponent<Renderer>();
				tow.transform.position = new Vector3 (12f*i, 4.5f, 12f*j);
				Material towMat = Resources.Load("Materials/piedra", typeof(Material)) as Material;
				towRenderer.material = towMat;				
				tow.transform.parent = par.transform;
				
				GameObject cupule = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				cupule.transform.localScale = new Vector3(5f, 5f, 5f);
				cupule.transform.position = new Vector3(12f*i, 9f, 12f*j);
				var cupuleRenderer4 = cupule.GetComponent<Renderer>();
				cupuleRenderer4.material = towMat;
				cupule.transform.parent = tow.transform;
		
			}
		}
	} 
	
	string generate_tile(int i, int j, int[,] map)
	{
		//La posición 0,0 se genera aleatoriamente. Todas las demás, con sus vecinos.
		if (i == 0 && j == 0){
			int pos = (int)(Random.Range(0f, 16f));
			map[0,0] = pos;
			return "wt/"+pos;
		}
		int n = -1;
		int e = -1;
		if (i > 0){
			n = map[i-1,j]; 
		}
		if (j > 0){
			e = map[i,j-1];
		}
		int tpos = check_valid(n,e);
		map[i,j] = tpos;
		return "wt/"+ (tpos);
		
	}
	

	int check_valid(int north, int east){
		
		int[] validnort = {-1};
		int[] valideas = {-1};
		
		if(north != -1 && (north/2)%4 == 0){
			int[] validnorth = {0,2,4,6};
			validnort = validnorth;
		}else if(north != -1 && (north/2)%4 == 1){
			int[] validnorth = {1,3,5,7};
			validnort = validnorth;
		}else if(north != -1 && (north/2)%4 == 2){
			int[] validnorth = {8,10,12,14};
			validnort = validnorth;
		}else if (north != -1){
			int[] validnorth = {9,11,13,15};
			validnort = validnorth;		
		}
		
		if(east != -1 && east%4 == 0){
			int[] valideast = {0,1,2,3};
			valideas = valideast;
		}else if(east != -1 && east%4 == 1){
			int[] valideast = {8,9,10,11};
			valideas = valideast;
		}else if(east != -1 && east%4 == 2){
			int[] valideast = {4,5,6,7};
			valideas = valideast;
		}else if(east != -1){
			int[] valideast = {12,13,14,15};
			valideas = valideast;	
		}
		
		int[] finalarray = mixit(validnort,valideas);
		int pos = (int)(Random.Range(0f, (float) finalarray.Length));
		return finalarray[pos];
		
	}		
	
	int[] mixit(int[] valn, int[] vale){
		if (valn[0] == -1){
			return vale;
		}
		if (vale[0] == -1){
			return valn;
		}
		int counter = 0;
		int[] rta = new int[2];
		for (int k = 0; k < 4; k++){
			int exhba = valn[k];
			for(int q = 0; q < 4; q++){
				if (exhba == vale[q]){
					rta[counter] = exhba;
					counter++;
				}
			}
		}
		return rta;
	}
	
	void setState(int newState)
	{
		if (newState == -3){
			return;
		}else if(newState == 3){
			return;
		}
		
		state = newState;
		if (state == -2){
			Transform cam = Camera.main.GetComponent<Transform>();
			cam.position = new Vector3(4f,2.7f,2);
			cam.transform.rotation = Quaternion.Euler(15f,-110f,0);
		}else if (state == -1){
			Transform cam = Camera.main.GetComponent<Transform>();
			cam.position = new Vector3(5f,2.7f,1);;
			cam.transform.rotation = Quaternion.Euler(15f,-100f,0);
		}else if (state == -0){
			Transform cam = Camera.main.GetComponent<Transform>();
			cam.position = new Vector3(6f,2.7f,0);
			cam.transform.rotation = Quaternion.Euler(15f,-90f,0);
		}else if (state == 1){
			Transform cam = Camera.main.GetComponent<Transform>();
			cam.position = new Vector3(5f,2.7f,-1);
			cam.transform.rotation = Quaternion.Euler(15f,-80f,0);
		}else if (state == 2){
			Transform cam = Camera.main.GetComponent<Transform>();
			cam.position = new Vector3(4f,2.7f,-2);
			cam.transform.rotation = Quaternion.Euler(15f,-70f,0);
		}
		
	}
	
	void luces(){
		
		luzroja = new GameObject("The Light 1");
        Light lightComp = luzroja.AddComponent<Light>();
        lightComp.color = Color.red;
		lightComp.type = LightType.Directional;
		lightComp.intensity =  0.7f;
        luzroja.transform.position = new Vector3(0, 4, 12);
		luzroja.transform.rotation = Quaternion.Euler(0,180f,0);
		
		luzazul = new GameObject("The Light 2");
        Light lightComp1 = luzazul.AddComponent<Light>();
        lightComp1.color = Color.blue;
		lightComp1.type = LightType.Directional;
		lightComp1.intensity =  0.7f;
        luzazul.transform.position = new Vector3(0, 4, -12);
		
		
		GameObject luzverde = new GameObject("The Light 3");
        Light lightComp2 = luzverde.AddComponent<Light>();
        lightComp2.color = Color.green;
        luzverde.transform.position = new Vector3(0, 3, 0);
		
	}
	
	void letrero(){
		
		GameObject luzletr = new GameObject("The letrero light");
        Light lightComp = luzletr.AddComponent<Light>();
        lightComp.color = Color.white;
        luzletr.transform.position = new Vector3(-9f, 4f, 0);
		
		letr = GameObject.CreatePrimitive(PrimitiveType.Cube);
		letr.transform.localScale = new Vector3 (0.5f, 4f, 10f);
		var letrRenderer = letr.GetComponent<Renderer>();
		letr.transform.position = new Vector3 (-10.5f, 4.5f, 0);
		Material letrMat = Resources.Load("Materials/letrero", typeof(Material)) as Material;
		letrRenderer.material = letrMat;
				
		GameObject cupule = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		cupule.transform.localScale = new Vector3(0.3f, 2f, 0.3f);
		cupule.transform.position = new Vector3(-10.5f, 2f, 0);
		var cupuleRenderer4 = cupule.GetComponent<Renderer>();
		Material metMat = Resources.Load("Materials/metal", typeof(Material)) as Material;
		cupuleRenderer4.material = metMat;
		cupule.transform.parent = letr.transform;
	}
	
	void crowd(){
		
		int crowded = (int)(Random.Range(0f, 3f));
		
		if (crowded == 0){
			fan1.GetComponent<Animator>().SetTrigger("Jump");
			return;
		}if (crowded == 1){
			fan2.GetComponent<Animator>().SetTrigger("Jump");
			return;
		}if (crowded == 2){
			fan3.GetComponent<Animator>().SetTrigger("Jump");
			return;
		}
	}
	
	void ganorojo(){
		luzazul.GetComponent<Light>().intensity =  0.0f;
		var letrRenderer = letr.GetComponent<Renderer>();
		Material letrMat = Resources.Load("Materials/rojogana", typeof(Material)) as Material;
		letrRenderer.material = letrMat;
	}
	
	void ganoazul(){
		luzroja.GetComponent<Light>().intensity =  0.0f;
		var letrRenderer = letr.GetComponent<Renderer>();
		Material letrMat = Resources.Load("Materials/azulgana", typeof(Material)) as Material;
		letrRenderer.material = letrMat;
	}
	

	void Update(){
		
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				setState(state+1);
			}
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				setState(state-1);
			}
		
		
		if (alive){
			if (Input.GetKeyDown(KeyCode.Space))
			{
				crowd();
			}
			if (Input.GetKeyDown(KeyCode.Z))
			{
				float dmg = Random.Range(5f, 11f);
				vidazul = vidazul - dmg;
				manoroja.GetComponent<Animator>().SetTrigger("Hit");
				if (vidazul <= 0){
					ganorojo();
					alive = false;
				}
			}
			if (Input.GetKeyDown(KeyCode.M))
			{
				float dmg = Random.Range(5f, 11f);
				vidaroja = vidaroja - dmg;
				manoazul.GetComponent<Animator>().SetTrigger("Hit");
				if (vidaroja <= 0){
					ganoazul();
					alive = false;
				}
			}
		}
		else{ crowd(); }
		
	}

}
