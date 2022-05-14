namespace Assets.AI.Detection
{
    public interface IColliderChecker
    {
        bool CollidingBootom { get; }

        bool CollidingLeft { get; }

        bool CollidingRight { get; }

        bool HasGroundLeft { get; }

        bool HasGroundRight { get; }
    }
}
