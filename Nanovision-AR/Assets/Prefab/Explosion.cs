using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public Material red;
    private GameObject[] getCount;
    public float cubeSize = 0.125f;
    public int cubesInRow = 5;      // NOTE: if you click on the cube under Hierarchy, you will be able to change those values
    // if you change Cubes in Row to 2, it will spawn 8, if cubes in row to 4, will spawn 64, if cubes in row is 16 then 512 (at least thats what i think)

    float cubesPivotDistance;
    Vector3 cubesPivot;

    public float explosionForce = 20f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.5f;

    

    // Start is called before the first frame update
    void Start()
    {

//        getCount = GameObject.FindGameObjectsWithTag("Cube"); // get count of cubes spawned
//        int count = getCount.Length;
        cubesPivotDistance = cubeSize * cubesInRow / 2;

        cubesPivot = new Vector3(cubesPivotDistance,cubesPivotDistance,cubesPivotDistance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        explode();
    }

    public void explode(){
        gameObject.SetActive(false);

        for(int x = 0; x < cubesInRow; x++) {
            for(int y = 0; y < cubesInRow; y++) {
                for(int z = 0; z < cubesInRow; z++){
                    createCube(x,y,z);
                }
            }
        }
        Vector3 explosionpos = transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionpos,explosionRadius);

        foreach(Collider hit in colliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }
    }
    /*public int RandomNumber(int min,int max)  
    {  
        return _random.Next(0, 255);  
    }  
    */
    
    void createCube(int x,int y,int z){
        GameObject cube;

        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cube.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        cube.transform.localScale = new Vector3(cubeSize,cubeSize,cubeSize);    // 1 / 8

        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Rigidbody>().mass = cubeSize;

        System.Random random = new System.Random();

        //int num = random.Next(255); 

        cube.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); // commented lines of code is me failing lol


        //cube.GetComponent<Renderer>().material.color = new Color(random.Next(255),random.Next(255),random.Next(100));
        //cube.GetComponent<Renderer>().material.color = new Color(240,38,20);
        //cube.transform.GetComponent<Renderer>().material = gameObject.GetComponent<Renderer>().material;
        // var randomColor = rndColor[new Random().Next(0,rndColor.Length)]
    }
}
