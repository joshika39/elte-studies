package game.vehicles;

import game.utils.*;

public abstract class Vehicle {

    protected final int id;
    private double currentSpeed;

    public double getSpeed(){
        return currentSpeed;
    }

    public Vehicle(int id) {
        this.id = id;
    }

    protected void accelerateCurrentSpeed(int delta){
        if(currentSpeed - delta < 0){
            throw new VehicleException();
        }
    }

    public abstract void accelerate(int amount);
}
