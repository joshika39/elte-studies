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

    public Circle(double x, double y, double radius) {
        super(x, y);

        if(radius == 0){
            throw new IllegalArgumentException("Radius cannot be zero");
        }
        this.radius = radius;
    }

    @Override
    public String toString() {
        return "Circle";
    }
}
