namespace ObjektLab1.Models
{
    public class StandardMedlem : Medlem
    {
        protected StandardMedlem()
        {
        }

        public StandardMedlem(int medlemsnummer, string namn, string epost, string mobilnummer)
            : base(medlemsnummer, namn, epost, mobilnummer)
        {
        }

        public override string Medlemstyp => "Ordinarie medlem";
    }
}
