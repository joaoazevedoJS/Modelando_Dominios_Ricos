using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests {
  [TestClass]
  public class StudentTests {
    
    private readonly Name _name;
    private readonly Email _email;
    private readonly Document _document;
    private readonly Address _address;
    private readonly Student _student;
    private readonly Subscription _subscription;

    public StudentTests() {
      this._name = new Name("João", "Azevedo");
      this._document = new Document("34110468000150", EDocumentType.CPF);
      this._email = new Email("devaslkdf@gmail.com");
      this._address = new Address("Rua 1", "1234", "Developer", "DEVS", "DV", "DVs", "404");
      this._student = new Student(this._name, this._document, this._email);
      this._subscription = new Subscription(null);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription() {
      var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "João Azevedo", this._document, this._address, this._email);
    
      this._subscription.AddPayment(payment);
      this._student.AddSubscription(this._subscription);
      this._student.AddSubscription(this._subscription);

      Assert.IsTrue(this._student.Invalid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscriptionHasNoPayment() {
      this._student.AddSubscription(this._subscription);

      Assert.IsTrue(this._student.Invalid);
    }

    [TestMethod]
    public void ShouldReturnSucessWhenAddSubscription() {
      var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "João Azevedo", this._document, this._address, this._email);
    
      this._subscription.AddPayment(payment);
      this._student.AddSubscription(this._subscription);

      Assert.IsTrue(this._student.Invalid);
    }
  }
}
