using FluentValidation;
using FluentValidation.Results;
using MVC.Business.Interfaces;
using MVC.Business.Models;
using MVC.Business.Notifications;

namespace MVC.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notificar(ValidationResult validationResult)
            => validationResult.Errors.ForEach(erro => Notificar(erro.ErrorMessage));

        protected void Notificar(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }

        protected bool ExecutarValidacao<TValidacao, TEntidade>(TValidacao validacao, TEntidade entidade)
            where TValidacao : AbstractValidator<TEntidade>
            where TEntidade : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}
