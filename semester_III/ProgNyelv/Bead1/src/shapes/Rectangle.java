package shapes;

public class Rectangle extends AShape {

    private final double length;

    public Rectangle(double x, double y, double length) {
        super(x, y);
        this.length = length;
    }

    @Override
    public double getLength() {
        return length;
    }

    @Override
    public double calculateArea() {
        return Math.pow(length,2);
    }
}
