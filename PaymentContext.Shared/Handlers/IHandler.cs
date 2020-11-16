using PaymentContext.Shared.Commands;

namespace PaymentContext.Shared.Handlers {
  interface IHandler<T> where T : ICommand {
    ICommandResult Handle(T command)    
  } 
}