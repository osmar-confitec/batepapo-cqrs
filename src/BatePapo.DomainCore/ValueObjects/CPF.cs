using BatePapo.CrossCutting;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BatePapo.DomainCore.ValueObjects
{
    public class CPF : Document
    {
        public CPF(string doc) : base(TypeDocument.PhysicalPerson, doc)
        {



        }


        public async override Task<bool> EhValido() => await ValidEntity(new ValidCPF());

        public override string GetDocumentFormatted() => MetodosComuns.FormatCPF(this.Doc);

        public override string GetDocumentNotFormatted() => MetodosComuns.SomenteNumeros(this.Doc);

        public override bool Equals(object obj)
        {
            var compareTo = obj as CPF;

            if (this.GetDocumentNotFormatted() == compareTo.GetDocumentNotFormatted())
                return false;

            return base.Equals(obj);
        }

        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Ef Constructor
        /// </summary>
        protected CPF() : base() { }

        public class ValidCPF : AbstractValidator<CPF>
        {

            public ValidCPF()
            {
               
                RuleFor(x => x.Doc)
                    .Must(x => x.ECpf()).WithMessage(" Atenção! CPF não respeita normas para validar. ");
            }

        }

    }
}
