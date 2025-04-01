namespace Jegymester.DataContext.Entities
{
    public class Role
    {
        public int Id { get; set; } //ez inkrementálódik mindig amikor reseteljük a database tesztadatokat
        public required int PermaId { get; set; } //ezt használjuk az "igazi" Id-ként (Igen, tudom hogy értelmetlennek tűnik, de ez van)
        public required string RoleName { get; set; }

        //Egy Role több User
        public List<User> Users { get; set; }
    }
}
