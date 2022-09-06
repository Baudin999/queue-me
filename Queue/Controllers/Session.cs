namespace Queue.Controllers;

public class Session 
{
    public DateTime TTL { get; set; }
    public Guid Id { get; }

    public Session(Guid id)
    {
        Id = id;
        TTL = DateTime.Now.Add(TimeSpan.FromMinutes(5));
    }

    public override bool Equals(object? obj)
    {
        if (obj is Session s) return s.Id == this.Id;
        else return false;
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }
}