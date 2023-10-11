package shapes;

public class Hexagon extends AShape implements Shape{
    private final double length;

    public Hexagon(double x, double y, double length) {
        super(x, y);
        this.length = length;
    }

    @Override
    public double getLength() {
        return length;
    }

    @Override
    public double calculateArea() {
        return Math.pow(length * 2, 2);
    }
}
