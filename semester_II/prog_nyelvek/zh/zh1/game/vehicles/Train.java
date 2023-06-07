package game.vehicles;

public class Train extends Vehicle{

    public Train(int id) {
        super(id);
    }

    @Override
    public void accelerate(int amount) {
        if(amount > 0){
            accelerateCurrentSpeed(amount/10);
        }
        else{
            accelerateCurrentSpeed(amount);
        }
    }
    
}
