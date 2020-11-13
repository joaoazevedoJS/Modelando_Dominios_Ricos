using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects {
  public class Email : ValueObject {
    public Email(string address) {
      this.Address = address;
    }
    
    public string Address { get; private set; }
  }    
}