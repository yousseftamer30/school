namespace DrivingSchoolFrontend.Models;

public class CustomerPaymentViewModel
{
    public int CustomerId { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string? NationalId { get; set; }
    public List<PaymentRecordViewModel> Payments { get; set; } = new();
}

public class PaymentRecordViewModel
{
    public int PaymentId { get; set; }
    public int LicenseId { get; set; }
    public string LicenseName { get; set; }
    public string ReceiptSerial { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string ReceiptStatus { get; set; }
    public bool HasReservation { get; set; }
    public int? ReservationId { get; set; }
}
