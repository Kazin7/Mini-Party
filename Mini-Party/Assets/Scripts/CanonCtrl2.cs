using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonCtrl2 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shotPos;
    private float randomNum;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer =0.0f;
        randomNum = Random.Range(1.0f,4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= randomNum){
            timer = 0;
            randomNum = Random.Range(1.0f,4.0f);
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = shotPos.transform.position;
            float force = Random.Range(300.0f,500.0f);
            bullet.GetComponent<Rigidbody>().AddForce(new Vector3(-force,100,0),ForceMode.Impulse);
        }
    }
}
