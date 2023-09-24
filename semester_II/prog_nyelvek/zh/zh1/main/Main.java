package main;

import game.Player;
import game.vehicles.Car;

import java.io.*;

public class Main {
    
    
    
    public static void main(String[] args){
        Player daniel = loadPlayerFromFile("Daniel");
        Player peter = loadPlayerFromFile("Peter");
        Player rich = loadPlayerFromFile("Richard");
        Player tom = loadPlayerFromFile("Tamas");
        Player zorror = loadPlayerFromFile("Zorror");

        Car car1 = new Car(0, 120, 1000);
        Car car2 = new Car(1, 150, 3000);
        Car car3 = new Car(2, 170, 4500);
        Car car4 = new Car(3, 170, 2500);
        Car car5 = new Car(4, 210, 10000);
    }

    public static Player loadPlayerFromFile(String playerName){
        File input = new File("users/" + playerName + ".txt");
    
        String[] data = null;
        try (BufferedReader bf = new BufferedReader(new FileReader(input))){
            String line = bf.readLine();
            data = line.split(" ");
    
            return new Player(playerName, data[1], Integer.parseInt(data[2]));
        } catch (IOException e) {
            System.out.println("IO error occured: " + e.getMessage());
        }
    
        return null;
    }
    
}
