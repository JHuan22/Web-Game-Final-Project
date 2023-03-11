[System.Serializable]
public class PlayerData
{
    public int health;

    public float[] position;

    public PlayerData(PlayerMovement player)
    {
        health = player.health;
        
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}