using Dapper.Contrib.Extensions;

namespace MotoRental.Borders.Models;
///<summary>
///Gerenciamento de sessão de usuário
///</summary>
public class Session
{
    private int _id;
    private string _name;
    private string _email;
    private string _phone;
    private bool _readonly;
    private bool _administrator;
    private string _password;
    private bool _disabled;
    private DateTime? _expirationTime;
    private int _deviceLimit;
    private int _userLimit;
    private bool _deviceReadonly;

    [Key]
    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public string Email { get => _email; set => _email = value; }
    public string Phone { get => _phone; set => _phone = value; }
    public bool Readonly { get => _readonly; set => _readonly = value; }
    public bool Administrator { get => _administrator; set => _administrator = value; }
    public bool DeviceReadonly { get => _deviceReadonly; set => _deviceReadonly = value; }
    public string Role { get => _deviceReadonly ? "readonly" : "admin"; }

}
