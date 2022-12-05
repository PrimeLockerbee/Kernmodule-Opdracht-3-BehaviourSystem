public interface IsSpotable
{
    public bool b_IsSpotted { get; set; }
}

public interface IsDamagable
{
    public void TakeDamage(int _damage)
    {
    }
}

public interface IsHealthUser : IsDamagable
{
    public int i_CurrentHealth { get; }

    public int i_MaxHealth { get; }
}