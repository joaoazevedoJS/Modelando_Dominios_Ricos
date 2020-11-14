using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects {
  public class Name : ValueObject {
    public Name(string firstName, string lastName) {
      this.FirstName = firstName;
      this.LastName = lastName;

      AddNotifications(new Contract()
        .Requires()
        .HasMinLen(this.FirstName, 3, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres")
        .HasMinLen(this.LastNmae, 3, "Name.LastNmae", "Sobrenome deve conter pelo menos 3 caracteres")
        .HasMinLen(this.FirstName, 40, "Name.FirstName", "Nome deve conter até 40 caracteres")
      );
    }
    
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
  }    
}