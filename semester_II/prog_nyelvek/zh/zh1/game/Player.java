package game;

public class Player{
    public String Name;
    public String IpAddress;
    public int Balance;
    
    public Player(String name, String ipAddress, int balance){
        if(name == null){
            throw new IllegalArgumentException("Playername cannot be null.");
        }
        if(ipAddress == null || ipAddress.length() <= 0 || ipAddress.contains("\t") || ipAddress.contains(" ") || ipAddress.contains("\n")){
            throw new IllegalArgumentException("Incorrect IP address");
        }
    }
}