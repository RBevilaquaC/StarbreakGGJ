using UnityEngine;

namespace Enemy
{
    public class DropSystem : MonoBehaviour
    {
        [SerializeField] private GameObject[] dropable;
        [SerializeField] private GameObject content;
        [SerializeField] private float rangeDrop;
        
        public void DropItem(int index, Vector2 position)
        {
            Vector2 dropPositon;
            dropPositon.x = position.x + Random.Range(-rangeDrop,rangeDrop);
            dropPositon.y = position.y + Random.Range(-rangeDrop,rangeDrop);
            
            Instantiate(dropable[index],dropPositon, Quaternion.identity);
        }
    }
}
