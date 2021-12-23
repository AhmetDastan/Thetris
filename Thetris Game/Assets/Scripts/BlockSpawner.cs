using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*block havuzundan bi blok sec eger sectigin yerde block yoksa yeni blok olustur
  yok olan blocklari blok havuzuna ekle 
   block spawn ile iligli her turlu isi burada yap 
 */


public class BlockSpawner : MonoBehaviour
{

    public void SpawnBlock(GameObject gameObject, Vector2 position)
    {
        Instantiate(gameObject, position, Quaternion.identity);
    }
}
