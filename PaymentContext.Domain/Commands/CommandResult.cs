using PaymentContext.Domain.Commands;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands {
  public class CommandResult : ICommandResult {
    public CommandResult() {}

    public CommandResult(bool sucess, string message) {
      this.Sucess = sucess;
      this.Message = message;
    }
               
    public bool Sucess { get; set; }
    public string Message { get; set; }
  }
}