using BatePapo.CrossCutting;
using BatePapo.DomainCore.Events;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BatePapo.DomainCore.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; private set; }

        LNoty _notificacoes_erros;

        public IReadOnlyCollection<Noty> NotificacoesErros => _notificacoes_erros?.AsReadOnly();


        public void AdicionarNotificacaoErro(Noty noty)
        {
            _notificacoes_erros = _notificacoes_erros ?? new LNoty();
            _notificacoes_erros.Add(noty);
        }


        public void AdicionarNotificacaoErro(IEnumerable<Noty> noties)
        {
            foreach (var item in noties)
            {
                _notificacoes_erros.Add(item);
            }
        }


        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual async Task<bool> ValidEntity<T>(AbstractValidator<T> validationRules) where T : class
        {
            var result = await validationRules.ValidateAsync((this as T));

            ObterValidacoes(result.Errors);

            return result.IsValid;
        }

        public abstract Task<bool> EhValido();

        protected void ObterValidacoes(IList<ValidationFailure> Errors)
        {
            foreach (var item in Errors)
                AdicionarNotificacaoErro(new Noty { Message = item.ErrorMessage });
        }
    }
}
