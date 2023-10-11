import shapes.*;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

public class Main {
    public static void main(String[] args) {
        ArrayList<Shape> shapes = new ArrayList<>();

        try (BufferedReader br = new BufferedReader(new FileReader("./sikidomok.txt"))) {
            String line;
            while ((line = br.readLine()) != null) {
                String[] parts = line.split(" ");
                String type = parts[0];
                double x = Double.parseDouble(parts[1]);
                double y = Double.parseDouble(parts[2]);
                double length = Double.parseDouble(parts[3]);

                switch (type){
                    case "c":
                        shapes.add(new Circle(x, y, length));
                        break;
                    case "r":
                        shapes.add(new Rectangle(x, y, length));
                        break;
                    case "h":
                        shapes.add(new Hexagon(x, y, length));
                        break;
                    case "t":
                        shapes.add(new Triangle(x, y, length));
                        break;
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
        }

        double maxArea = 0;
        Shape idiom = null;

        for (Shape shape : shapes) {
            double area = shape.calculateArea();
            if (area > maxArea) {
                maxArea = area;
                idiom = shape;
            }
        }

        if (idiom != null) {
            System.out.println("A legnagyobb területű síkidom: " + idiom);
            System.out.println("Terület: " + maxArea);
        } else {
            System.out.println("Nincs megfelelő síkidom az elemzéshez.");
        }
    }
}