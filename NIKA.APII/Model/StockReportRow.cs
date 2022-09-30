namespace NIKA.APII.Model
{
    public record StockReportRow
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public short? UnitsInStock { get; set; }

        public bool IsCritic { get => UnitsInStock <= 5; }
        // bu şekilde set değil sadece get bir değer ve sadece okuma yaparken ufak bir if dönüyor içinde
    }
}
