namespace NetCoreAngularUdemy.ViewModel
{
    public class UsuarioPublic
    {
        public int userId { get; set; }
        public string username { get; set; }

        public UsuarioPublic(int userId, string username) {
            this.userId = userId;
            this.username = username;
        }
    }
}
