using BatePapo.CrossCutting;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BatePapo.DomainCore.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }


        LNoty _notificacoes_erros;

        public IReadOnlyCollection<Noty> NotificacoesErros => _notificacoes_erros?.AsReadOnly();


        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public void AdicionarNotificacaoErro(Noty noty)
        {
            _notificacoes_erros = _notificacoes_erros ?? new LNoty();
            _notificacoes_erros.Add(noty);
        }


        public void AdicionarNotificacaoErro(IEnumerable<Noty> noties)
        {
            foreach (var item in noties)
            {
                AdicionarNotificacaoErro(item);
            }
        }


        protected virtual async Task<bool> ValidEntity<T>(AbstractValidator<T> validationRules) where T : class
        {
            var result = await validationRules.ValidateAsync((this as T));

            ObterValidacoes(result.Errors);

            return result.IsValid;
        }


        protected void ObterValidacoes(IList<ValidationFailure> Errors)
        {
            foreach (var item in Errors)
                AdicionarNotificacaoErro(new Noty { Message = item.ErrorMessage });
        }



        public abstract Task<bool> EhValido();



        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }




    }
}
