    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Animations;
    using UnityEngine.UIElements;

    public class BulletPoison : MonoBehaviour
    {
        public Vector3 shootDirection;
        public float bulletSpeed = 1.0f;

        public float bulletForce = 1f;
        private Rigidbody rb;
        public GameObject smokePrefab;
        private int currentPierceCount = 0;
        public float smokeDuration = 3f;
        public BigCatSlimeShoot shootScript;
        
        private List<Vector3> hitCellPositions = new List<Vector3>();
        private void Start()
        {
            float randomY = Random.Range(.5f, 1f);
            rb = GetComponent<Rigidbody>();
            shootDirection.y = randomY;
            Vector3 initialVelocity = shootDirection * bulletSpeed * bulletForce;
            rb.velocity = initialVelocity;
            shootScript = GetComponentInParent<BigCatSlimeShoot>();
        }
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("gridCell"))
            {
                gridCell cell = other.GetComponent<gridCell>();
                Vector3 cellPosition = cell.GetCellPosition();
                hitCellPositions.Add(cellPosition);
                if (!cell.isOccupied() && hitCellPositions.Count != 0)
                {
                    CreateSmoke(hitCellPositions[0]);
                }
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                print(other.gameObject.transform.position);
                Vector3 enemyPos = other.gameObject.transform.position;
                print(enemyPos);
                Vector3 smokePos = new Vector3(enemyPos.x, 0, enemyPos.z);
                CreateSmoke(smokePos);

            }
            else if(!other.gameObject.CompareTag("gridCell"))
            {
                Destroy(gameObject);
            }
        }
        
        private void CreateSmoke(Vector3 cellPosition)
        {
            Vector3 origPos = cellPosition;
            Vector3 frontPos = new Vector3(origPos.x, origPos.y, origPos.z - 1f);
            Vector3 backPos = new Vector3(origPos.x, origPos.y, origPos.z + 1f);
            Instantiate(smokePrefab, cellPosition, Quaternion.identity);
            if (shootScript.isLvl2)
            {
                Instantiate(smokePrefab, frontPos, Quaternion.identity);
            }

            if (shootScript.isLvl3)
            {
                Instantiate(smokePrefab, backPos, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        
        
    }
