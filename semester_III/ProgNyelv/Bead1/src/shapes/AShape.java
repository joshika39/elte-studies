package shapes;

public abstract class AShape implements Shape {
    protected double x;
    protected double y;
    @Override
    public double getX() {
        return x;
    }

    @Override
    public double getY() {
        return y;
    }

    public AShape(double x, double y) {
        this.x = x;
        this.y = y;
    }
}
