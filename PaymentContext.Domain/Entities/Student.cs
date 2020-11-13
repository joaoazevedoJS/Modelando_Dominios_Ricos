using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities {
  public class Student : Entity {
    private IList<Subscription> _subscriptions;
    
    public Student(Name name, Document document, Email email) {
      this.Name = name;
      this.Document = document;
      this.Email = email;
      this._subscriptions = new List<Subscription>();

      AddNotification(name, document, email);
    }

    public Name Name { get; private set; }
    public Document Document { get; private set; }
    public Email Email { get; private set; }
    public Address Address { get; private set; }
    public IReadOnlyCollection<Subscription> Subscriptions { get { 
      return _subscriptions.ToArray();
    } }
  
    public void AddSubscription(Subscription subscription) {
      foreach (var sub in this.Subscriptions) {
        sub.Inactivate();
      }

      this._subscriptions.Add(subscription);
    }
  }
}