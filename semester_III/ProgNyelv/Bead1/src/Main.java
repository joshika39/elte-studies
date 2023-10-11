import shapes.Circle;
import shapes.Shape;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

public class Main {
    public static void main(String[] args) {
        ArrayList<Shape> shapes = new ArrayList<>();

        try (BufferedReader br = new BufferedReader(new FileReader("sikidomok.txt"))) {
            String line;
            while ((line = br.readLine()) != null) {
                String[] parts = line.split(" ");
                String type = parts[0];
                double x = Double.parseDouble(parts[1]);
                double y = Double.parseDouble(parts[2]);
                double length = Double.parseDouble(parts[3]);
                shapes.add(new Circle(x, y, length));
            }
        } catch (IOException e) {
            e.printStackTrace();
        }

        double maxArea = 0;
        Shape largestRectangle = null;

        for (Shape shape : shapes) {
            double area = shape.calculateArea();
            if (area > maxArea) {
                maxArea = area;
                largestRectangle = shape;
            }
        }

        if (largestRectangle != null) {
            System.out.println("A legnagyobb területű síkidom: " + largestRectangle.type);
            System.out.println("Terület: " + maxArea);
        } else {
            System.out.println("Nincs megfelelő síkidom az elemzéshez.");
        }
    }
}