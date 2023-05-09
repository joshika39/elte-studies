package gyak7;

public class Point {
    public final int x, y;
    public Point( int x, int y ){
        this.x = x;
        this.y = y;
    }

    public boolean equals( Point that ){
        return that != null && x == that.x && y == that.y;
    }

    public static void main( String... args ){
        Point p = new Point(4,2);
        Point q = new Point(4,2);
        System.out.print( p == q );
        System.out.print( p.equals(q) );
    }
}