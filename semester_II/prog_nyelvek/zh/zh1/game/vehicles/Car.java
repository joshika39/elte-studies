package game.vehicles;

public class Car extends Vehicle {
    public final int MaxSpeed;
    public final int Price;
    
    public Car(int id, int maxSpeed, int price) {
        super(id);
        MaxSpeed = maxSpeed;
        Price = price;
    }


    @Override
    public void accelerate(int amount) {
        if(amount < MaxSpeed){
            accelerateCurrentSpeed(amount);
        }
    }

    public String toString(){
        return String.format("ID: %d, Price: %d, Max speed: %d", id, Price, MaxSpeed);
    }
    
}
