using UnityEngine;

[CreateAssetMenu(fileName = "Bullet_",  menuName = "Scriptable Objects/Bullet Stats", order = 0)]
public class BulletStats : ScriptableObject
{
    public float fireRate = 5;
    public float speed = 2;
    public float damage = 1;
    public float lifetime = 5;
    
    public enum Movement {Straight}
    public Movement movement = Movement.Straight;
}
