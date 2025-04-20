using System.Numerics;

public enum TypeNotificationArrow
{
    None,
    PlayerAttacking,
    PlayerAttacked,
    EnemiesAttackingThemself,
    Count
}

public class NotificationArrow : Notification
{
    public readonly TypeNotificationArrow typeNotificationArrow;
    public readonly Vector2 v2_OriginPosition;

    public NotificationArrow(Character originCharacter, Character targetCharacter, float f_Delay, TypeNotificationArrow typeNotificationArrow) :
        base(targetCharacter.GetPosition(), f_Delay)
    {
        this.typeNotificationArrow = typeNotificationArrow;
        b_IsTriggered = true;

        // Retrieve the position of the center of the characterOrigin and Target
        v2_OriginPosition = RetrieveCenterPosition(originCharacter);
        v2_Position = RetrieveCenterPosition(targetCharacter);
    }

    private Vector2 RetrieveCenterPosition(Character character)
    {
        Vector2 v2_CenterOfBoard;

        v2_CenterOfBoard.X = character.GetPosition().X + (GameInfo.i_nbCol * GameInfo.i_SizeCell * character.GetZoom() / 2);
        v2_CenterOfBoard.Y = character.GetPosition().Y + (GameInfo.i_nbLin * GameInfo.i_SizeCell * character.GetZoom() / 2);

        return v2_CenterOfBoard;
    }
}