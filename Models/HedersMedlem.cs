namespace ObjektLab1.Models
{
    public class HedersMedlem : Medlem
    {
        protected HedersMedlem()
        {
        }

        public HedersMedlem(int medlemsnummer, string namn, string epost, string mobilnummer)
            : base(medlemsnummer, namn, epost, mobilnummer)
        {
        }

        public override string Medlemstyp => "Hedersmedlem";
    }
}
