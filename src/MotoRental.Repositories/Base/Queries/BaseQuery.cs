namespace MotoRental.Repositories.Base.Queries;

public struct BaseQuery
{
    public static string Update => "update {0} set {1} where {2}";
    public static string Insert => "insert into {0} ({1}) values ({2})";
    public static string Delete => "delete from {0} where id = {1}";
    public static string Select => "select {1} from {0} where {2}";
}