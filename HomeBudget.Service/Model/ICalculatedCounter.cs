namespace HomeBudget.Service.Model
{
    public interface ICalculatedCounter
    {
        decimal Increase { get; set; }
        decimal Surcharge { get; set; }
    }
}