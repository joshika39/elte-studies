package linear.algebra;

public class GaussianElimination {

    private static char[] variables = {'x', 'y', 'z', 'q', 'a', 'b', 'c'};
    private final int _rows;
    private final int _cols;
    private double[][] _matrix;

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
        if(matrix.length != _rows || matrix[0].length != _cols){
            throw new IllegalArgumentException();
        }
        _matrix = matrix;
    }

    public GaussianElimination(int rows, int cols, double[][] matrix) {
        _rows = rows;
        _cols = cols;
        _matrix = matrix;
    }

    public GaussianElimination(int rows, String[] data) {
        _rows = rows;
        _cols = data.length / rows;
        _matrix = new double[_rows][_cols];

        for (int i = 0; i < _rows; i++) {
            for (int j = 0; j < _cols; j++) {
                _matrix[i][j] = Double.parseDouble(data[j + _cols * i]);
            }
        }
    }

//    public void rowEchelonForm() {
//        int lead = 0;
//
//        for (int r = 0; r < _rows; r++) {
//            if (_cols <= lead){
//                return;
//            }
//            int i = r;
//
//            while (_matrix[i][lead] == 0){
//                i++;
//                if(_rows == i){
//                    i = r;
//                    lead++;
//                    if(_cols == lead){
//                        return;
//                    }
//                }
//            }
//            if(i != r){ swapRows(i, r); }
//            multiplyRow(r, 1/_matrix[r][lead]);
//            for(int j = 0; j < _rows; j++){
//                if(j != r){
//                    multiplyAndAddRow(r, j,((-1) * _matrix[j][lead]));
//                }
//            }
//            lead++;
//        }
//
//    }

    public void rowEchelonForm() {
        double [][] A = _matrix;
        int m = _rows;
        int n = _cols;
        int h = 0;
        int k = 0;

        while (h < m && k < n){
            int i_max = argMax(h, k);
            if(A[i_max][k] == 0){
                k++;
                h++;
            }
            else{
                swapRows(h, i_max);
                for (int i = h + 1; i < m; i++) {
                    double f = A[i][k] / A[h][k];
                    A[i][k] = 0;
                    for (int j = k + 1; j < n; j++) {
                        A[i][j] -= A[h][j] * f;
                    }
                }
                h++;
                k++;
            }
        }
    }

    private int argMax(int h, int k){
        double [][] A = _matrix;
        int iMax = h;
        double max = Math.abs(A[h][k]);
        for(int i = h; i < _rows; i++){
            if(Math.abs(A[i][k]) >= max){
                iMax = i;
                max = Math.abs(A[i][k]);
            }
        }
        return iMax;
    }

    public void backSubstitution(){
        for (int i = 0; i < _matrix.length; i++) {
            for (int j = 0; j < _matrix[0].length; j++) {
                if(_matrix[i][j] == 0){
                    throw new IllegalArgumentException();
                }
            }
        }
    }

    /**
     * Swap positions of 2 rows
     *
     * @param rowIndex1 int index of row to swap
     * @param rowIndex2 int index of row to swap
     *
     */
    private void swapRows(int rowIndex1, int rowIndex2){
        // number of columns in matrix
        int numColumns = _matrix[0].length;

        // holds number to be swapped
        double hold;

        for(int k = 0; k < numColumns; k++){
            hold = _matrix[rowIndex2][k];
            _matrix[rowIndex2][k] = _matrix[rowIndex1][k];
            _matrix[rowIndex1][k] = hold;
        }
    }

    /**
     * Adds 2 rows together row2 = row2 + row1
     *
     * @param rowIndex1 int index of row to be added
     * @param rowIndex2 int index or row that row1 is added to
     *
     */
    private void rowAdd(int rowIndex1, int rowIndex2){
        // number of columns in _matrix
        int numColumns = _matrix[0].length;

        for(int k = 0; k < numColumns; k++){
            _matrix[rowIndex2][k] += (double)_matrix[rowIndex1][k];
        }
    }

    /**
     * Multiplies a row by a scalar
     *
     * @param rowIndex int index of row to be scaled
     * @param scalar double to scale row by
     *
     */
    private void multiplyRow(int rowIndex, double scalar){
        // number of columns in _matrix
        int numColumns = _matrix[0].length;

        for(int k = 0; k < numColumns; k++){
            _matrix[rowIndex][k] *= scalar;
        }
    }

    /**
     * Adds a row by the scalar of another row
     * row2 = row2 + (row1 * scalar)
     * @param rowIndex1 int index of row to be added
     * @param rowIndex2 int index or row that row1 is added to
     * @param scalar double to scale row by
     *
     */
    private void multiplyAndAddRow(int rowIndex1, int rowIndex2, double scalar){
        int numColumns = _matrix[0].length;

        for(int k = 0; k < numColumns; k++){
            _matrix[rowIndex2][k] += ((double)_matrix[rowIndex1][k] * scalar);
        }
    }

    public void print() {
        print(_matrix);
    }

    public static void print(double[][] matrix) {
        for (int i = 0; i < matrix.length; i++) {
            for (int j = 0; j < matrix[0].length; j++) {
                double num = matrix[i][j];
                String text;
                if (matrix[i].length - 1 == j) {
                    text = String.format("=%.2f", num);
                } else {
                    if (num < 0) {
                        text = String.format("%.2f%c", num, variables[j]);
                    } else {
                        text = String.format("%c%.2f%c", '+', num, variables[j]);
                    }
                }
                System.out.print(text);

            }
            System.out.println();
        }
        System.out.println();
    }
}
