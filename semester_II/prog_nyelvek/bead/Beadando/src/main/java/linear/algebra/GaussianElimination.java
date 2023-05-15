package linear.algebra;

public class GaussianElimination {
    private int _rows;
    private int _cols;
    private double _matrix[][];

    public int getCols() {
        return _cols;
    }

    public int getRows() {
        return _rows;
    }

    public double[][] getMatrix() {
        return _matrix;
    }

    public void setMatrix(double[][] matrix) {
        _matrix = matrix;
    }

    public GaussianElimination(int rows, int cols, double[][] matrix) {
        _rows = rows;
        _cols = cols;
        _matrix = matrix;
    }

    public void rowEchelonForm(){

    }

    public void backSubstitution(){

    }
}
