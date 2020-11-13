using PaymentContext.Shared.ValueObjects;
using PaymentContext.Domain.Enums;

namespace PaymentContext.Domain.ValueObjects {
  public class Document : ValueObject {
    public Document(string number, EDocumentType type) {
      this.Number = number;
      this.Type = type;
    }
    
    public string Number { get; private set; }
    public EDocumentType Type { get; set; }
  }    
}