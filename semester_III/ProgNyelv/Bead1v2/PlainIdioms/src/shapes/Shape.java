package shapes;

/**
 * A strategy pattern implemented with shapes
 */
public interface Shape {
    /**
     * Coordinate of the center point on the X axis
     * @return bool
     */
    double getX();

    /**
     * Coordinate of the center point on the Y axis
     * @return bool
     */
    double getY();

    /**
     * Gets the length of plain idiom
     * @return bool
     */
    double getLength();

    /**
     * Overlapping rectangle
     * @return A calculated size
     */
    double calculateArea();

}
