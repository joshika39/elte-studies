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


}
