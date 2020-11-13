using System;
// using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities {
  public class CreditCardPayment : Payment {
    public CreditCardPayment(
      string cardHolderName, 
      string cardLastForNumber, 
      string lastTransactionNumber, 
      DateTime paidDate, 
      DateTime expireDate, 
      decimal total, 
      decimal totalPaid, 
      string payer, 
      Document document,
      Address address, 
      Email email
    ) : base(paidDate, expireDate, total, totalPaid, payer, document, address, email) {
      this.CardHolderName = cardHolderName;
      this.CardLastForNumber = cardLastForNumber;
      this.LastTransactionNumber = lastTransactionNumber;
    }

    public string CardHolderName { get; private set; }
    public string CardLastForNumber { get; private set; }
    public string LastTransactionNumber { get; private set; }
  }
}