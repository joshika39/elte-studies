package shapes;

public class Circle extends AShape{

    private double radius;
    @Override
    public double getLength() {
        return radius;
    }

    @Override
    public double calculateArea() {
        return Math.pow(radius * 2, 2);
    }

    public Circle(double radius) {
        this.radius = radius;
    }
}
