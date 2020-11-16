using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers {
  public class SubscriptionHandler : 
    Notifiable, 
    IHandler<CreateBoletoSubscriptionCommand>, 
    IHandler<CreatePayPalSubscriptionCommand> 
  {
    private readonly IStudentRepository _repository;
    private readonly IEmailService _emailService;

    public SubscriptionHandler(IStudentRepository repository, IEmailService emailService) {
      this._repository = repository;
      this._emailService = emailService;
    }

    public ICommandResult Handle(CreateBoletoSubscriptionCommand command) {
      // Fail Fast Validations
      command.Validate();
      
      if(command.Invalid) {
        AddNotifications(command);

        return new CommandResult(false, "Não foi possível realizar sua assinatura");
      } 

      // Verificar se documento já está cadastrado!
      if(this._repository.DocumentExists(command.Document)) {
        AddNotifications("Document", "Este CPF já está em uso");
      }

      // Verificar se e-mail já está cadastrado!
      if(this._repository.EmailExists(command.Email)) {
        AddNotifications("Email", "Este e-mail já está em uso");
      }

      // Gerar os VOs

      var name = new Name(command.FirstName, command.LastName);
      var document = new Document(command.Document, EDocumentType.CPF);
      var email = new Email(command.Email);
      var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
      
      // Gerar as Entidades
      var student = new Student(name, document, email);
      var subscription = new Subscription(DateTime.Now.AddMonths(1));
      var payment = new BoletoPayment(
        command.BarCode,
        command.BoletoNumber,
        command.PaidDate,
        command.ExpireDate,
        command.Total,
        command.TotalPaid,
        command.Payer,
        new Document(command.PayerDocument, command.PayerDocumentType),
        address,
        email
      );

      // Relacionamentos
      subscription.AddPayment(payment);
      student.AddSubscription(subscription);

      // Agrupar as Validações

      AddNotifications(name, document, email, address, student, subscription, payment);

      // Salvar as Informações
      this._repository.CreateSubscription(student);

      // Enviar E-mail de boas vindas
      this._emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao meu curso", "Sua assinatura feita com sucesso!");

      // Retorna as informações
      return new CommandResult(true, "Assinatura realizada com sucesso");
    }
    
    public ICommandResult Handle(CreatePayPalSubscriptionCommand command) {
      // Verificar se documento já está cadastrado!
      if(this._repository.DocumentExists(command.Document)) {
        AddNotifications("Document", "Este CPF já está em uso");
      }

      // Verificar se e-mail já está cadastrado!
      if(this._repository.EmailExists(command.Email)) {
        AddNotifications("Email", "Este e-mail já está em uso");
      }

      // Gerar os VOs

      var name = new Name(command.FirstName, command.LastName);
      var document = new Document(command.Document, EDocumentType.CPF);
      var email = new Email(command.Email);
      var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
      
      // Gerar as Entidades
      var student = new Student(name, document, email);
      var subscription = new Subscription(DateTime.Now.AddMonths(1));

      // Só muda a implementação do Pagamento
      var payment = new PayPalPayment(
        command.TransactionCode,
        command.PaidDate,
        command.ExpireDate,
        command.Total,
        command.TotalPaid,
        command.Payer,
        new Document(command.PayerDocument, command.PayerDocumentType),
        address,
        email
      );

      // Relacionamentos
      subscription.AddPayment(payment);
      student.AddSubscription(subscription);

      // Agrupar as Validações

      AddNotifications(name, document, email, address, student, subscription, payment);

      // Salvar as Informações
      this._repository.CreateSubscription(student);

      // Enviar E-mail de boas vindas
      this._emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao meu curso", "Sua assinatura feita com sucesso!");

      // Retorna as informações
      return new CommandResult(true, "Assinatura realizada com sucesso");
    }
    }
  }
}