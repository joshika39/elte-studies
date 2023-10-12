package shapes;

public class Rectangle extends AShape {

    private final double length;

    public Rectangle(double x, double y, double length) {
        super(x, y);

        if(length == 0){
            throw new IllegalArgumentException("Length cannot be zero");
        }
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

    @Override
    public String toString() {
        return "Rectangle";
    }
}
