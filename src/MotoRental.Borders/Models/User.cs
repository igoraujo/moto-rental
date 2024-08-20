using Dapper.Contrib.Extensions;

namespace MotoRental.Borders.Models;
///<summary>
///Usuários
///</summary>
public class User
{
    private int _id;
    private string _name;
    private string _email;
    private string _login;
    private string _phone;
    private bool _readonly;
    private bool _administrator;
    
    [Key]
    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public string Login { get => _login; set => _login = value; }
    public string Email { get => _email; set => _email = value; }
    public string Phone { get => _phone; set => _phone = value; }
    public bool Readonly { get => _readonly; set => _readonly = value; }
    public bool Administrator { get => _administrator; set => _administrator = value; }
    
}
