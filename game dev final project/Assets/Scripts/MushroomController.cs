using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public GameObject bulletPrefab;
    private float force = 5f;
    private int numBullets = 10;
    private float angle;
    private float angleInterval;
    private Vector2 bulletVelocity = new Vector2(1,1);
    private List<GameObject> pooledBullets  =  new  List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        angleInterval = 360/numBullets;
        instantiateBullets();
        StartCoroutine(spawnBullets());
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void FixedUpdate() {

    }

    void instantiateBullets() {
        for (int i =0; i<numBullets; i++) {
            GameObject bullet = (GameObject) Instantiate(bulletPrefab, transform.position + new Vector3(bulletVelocity.x, bulletVelocity.y, 0), Quaternion.identity);
            bullet.SetActive(false);
            bullet.transform.parent = this.transform;
            pooledBullets.Add(bullet);
            angle += angleInterval;
        }
    }

    IEnumerator spawnBullets() {
        while (true) {
            angle = 0f;
            for (int i=0; i<numBullets; i++) {
                bulletVelocity = new Vector2(Mathf.Cos(angle*(Mathf.PI/180)), Mathf.Sin(angle*(Mathf.PI/180)));
                GameObject bullet = null;
                for (int k = 0; k<pooledBullets.Count; k++) {
                    if (!pooledBullets[k].gameObject.activeInHierarchy) {
                        bullet = pooledBullets[k];
                        break;
                    }
                }

                if (!bullet) {
                    bullet = (GameObject) Instantiate(bulletPrefab, transform.position + new Vector3(bulletVelocity.x, bulletVelocity.y, 0), Quaternion.identity);
                    bullet.transform.parent = this.transform;
                    pooledBullets.Add(bullet);
                }
                
                bullet.transform.position = bullet.transform.parent.position + new Vector3(bulletVelocity.x, bulletVelocity.y, 0);
                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody2D>().AddForce(bulletVelocity*force, ForceMode2D.Impulse);
                angle += angleInterval;

            }
            
            yield return new WaitForSeconds(0.5f);
        }
    }
}
