using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*block havuzundan bi blok sec eger sectigin yerde block yoksa yeni blok olustur
  yok olan blocklari blok havuzuna ekle 
   block spawn ile iligli her turlu isi burada yap 
 */


public class BlockSpawner : MonoBehaviour
{
    public BlockManager blockManager;
    [SerializeField] private GameObject[] blockPrefabs;

    public GameObject SpawnBlock()
    {
        return Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)], new Vector3(5f, 17f, 0), Quaternion.identity); ;
    }
}
