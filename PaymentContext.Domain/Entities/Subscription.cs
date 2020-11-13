using System;
using System.Collections.Generic;
using System.Linq;
// using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities {
  public class Subscription {
    private IList<Payment> _payments;

    public Subscription(DateTime? expireDate) {
      this.CreateDate = DateTime.Now;
      this.LastUpdateDate = DateTime.Now;
      this.ExpireDate = expireDate;
      this.Active = true;
      this._payments = new List<Payment>();
    }

    public DateTime CreateDate { get; private set; }
    public DateTime LastUpdateDate { get; private set; }
    public DateTime? ExpireDate { get; private set; }
    public bool Active { get; private set; }
    public IReadOnlyCollection<Payment> Payments { get; private set; }
  
    public void AddPayment(Payment payment) {
      this._payments.Add(payment);
    }

    public void Activate() {
      this.Active = true;
      this.LastUpdateDate = DateTime.Now;
    }
    
    public void Inactivate() {
      this.Active = false;
      this.LastUpdateDate = DateTime.Now;
    }
  }
}