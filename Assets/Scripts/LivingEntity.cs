using UnityEngine;

public class LivingEntity : MonoBehaviour
{

    public float Health; // Set this in the init
    public Type Entity_Type;

    public enum Type
    {
        MONSTER,
        FRIENDLY,
        NONE
    }
    

}