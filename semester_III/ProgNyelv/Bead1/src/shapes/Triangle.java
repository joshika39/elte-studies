package shapes;

public class Triangle extends AShape implements Shape {
    private final double length;

    public Triangle(double x, double y, double length) {
        super(x, y);
        this.length = length;
    }

    @Override
    public double getLength() {
        return length;
    }

    @Override
    public double calculateArea() {
        return Math.pow(length * Math.sqrt(3) / 2, 2);
    }
}
