using Project.Models;

namespace Project.Services.BeautyDesc;

public class BeautyDesc : IBeautyDesc
{
    public string RemoveDoubleSpace(string description)
    {
        return description.Replace("  ", " ");
    }

    public void MarkSpot(string description, ICollection<Spot> spots)
    {
        throw new NotImplementedException();
    }

    public void MarkTrip(string description, ICollection<Trip> trips)
    {
        throw new NotImplementedException();
    }

    public void TagUser(string description, ICollection<User> users)
    {
        throw new NotImplementedException();
    }
}