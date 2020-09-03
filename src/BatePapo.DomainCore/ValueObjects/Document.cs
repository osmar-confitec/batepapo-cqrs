using BatePapo.CrossCutting;
using BatePapo.DomainCore.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BatePapo.DomainCore.ValueObjects
{

    public enum TypeDocument
    { 
        PhysicalPerson = 1,
        LegalPerson = 2
    }

   public abstract class Document : Entity
    {

        public abstract string GetDocumentFormatted();

        public abstract string GetDocumentNotFormatted();

        public  string Doc { get; private set; }


        public override string ToString() => GetDocumentFormatted();


        protected string RemoveSpecialCharacters(string texto) => MetodosComuns.SomenteNumeros(texto);


        protected TypeDocument TipoDocumentoDoc { get; private set; }
        //EF Constructor
        protected Document() : base() { }

        protected Document(TypeDocument tipoDocumentoDoc, string doc)
        {
            TipoDocumentoDoc = tipoDocumentoDoc;
            Doc = doc;

        }



    }
}
