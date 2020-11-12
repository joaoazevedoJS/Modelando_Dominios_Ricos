using System;

namespace PaymentContext.Domain.Entities {
  public abstract class Payment {
    public Payment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string payer, string document, string address, string email) {
      Number = number
      PaidDate = paidDate
      ExpireDate = expireDate
      Total = total
      TotalPaid = totalPaid
      Payer = payer
      Document = document
      Address = address
      Email = email
    }

    public string Number { get; private set; }
    public DateTime PaidDate { get; private set; }
    public DateTime ExpireDate { get; private set; }
    public decimal Total { get; private set; }
    public decimal TotalPaid { get; private set; }
    public string Payer { get; private set; }
    public string Document { get; private set; }
    public string Address { get; private set; }
    public string Email { get; private set; }
  }
  
  public class BoletoPayment : Payment {
    public string BarCode { get; set; }
    public string BoletoNumber { get; set; }
  }
  
  public class CreditCardPayment : Payment {
    public string CardHolderName { get; set; }
    public string CardLastForNumber { get; set; }
    public string LastTransactionNumber { get; set; }
  }
  
  public class PayPalPayment : Payment {
    public string TransactionCode { get; set; }   
  }
}