namespace LabDB.Entity;

public class Agent
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Passsword { get; set; }

    public Agent()
    {
        Login = string.Empty;
        Passsword = string.Empty;
    }

    public Agent(string login, string passsword)
    {
        Login = login;
        Passsword = passsword;
    }
}