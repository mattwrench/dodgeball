using UnityEngine;

public class GameCharState : MonoBehaviour
{
    public enum Team
    {
        Left, Right
    }

    public enum AvatarType
    {
        Matt, Richard, Max, Eduardo, David, Jun, Ashley, Travis, MattGun
    }

    public Team Side;
    public AvatarType Avatar;
}
