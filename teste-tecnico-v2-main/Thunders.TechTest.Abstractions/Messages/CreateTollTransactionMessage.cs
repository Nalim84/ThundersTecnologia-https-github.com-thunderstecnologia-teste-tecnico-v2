namespace Thunders.TechTest.Abstractions.Messages;

public class CreateTollTransactionMessage
{
    public Guid Id { get; set; }
    public Guid TollId { get; set; }
    public VehicleType VehicleType { get; set; }
    public decimal AmountPaid { get; set; }

    public CreateTollTransactionMessage() { }

    public CreateTollTransactionMessage(Guid Id , Guid tollId, VehicleType vehicleType, decimal amountPaid)
    {
        TollId = tollId;
        VehicleType = vehicleType;
        AmountPaid = amountPaid;
    }
}


public enum VehicleType
{
    Motorcycle = 1,
    Car = 2,
    Truck = 3
}
