namespace ZoneModel.Services.Contracts
{
    public interface ISubnetCalculator
    {
        string GetSubnetOffset(string ip,byte mask,  int index);
    }
}