using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;

namespace paymentcontext.Tests {
  [TestClass]
  public class StudentTests {
    [TestMethod]
    public void AdicionarAssinatura() {
      var subscription = new Subscription(null);
      var student = new Student("João", "Azevedo", "123456789", "devjoaoazevedo@gmail.com");
      
      student.AddSubscription(subscription);
    }
  }
}
