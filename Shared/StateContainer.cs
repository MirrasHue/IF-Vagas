using IF_Vagas.Data;
namespace IF_Vagas.Shared;

public class StateContainer {
    public User? user { get; set; }
    public void UpadateUser(User user){
        this.user = user;
    }
}