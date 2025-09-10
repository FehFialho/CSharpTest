using Project.Models;
namespace Project.Services.BeautyDesc;

public interface IBeautyDesc
{
    // Remover Espaçamento Duplo
    string RemoveDoubleSpace(string description);

    // Marcar Usuários
    void TagUser(string description, ICollection<User> users);

    // Marcar Passeios
    void MarkTrip(string description, ICollection<Trip> trips);

    // Marcar Pontos Turisticos
    void MarkSpot(string description, ICollection<Spot> spots);
}