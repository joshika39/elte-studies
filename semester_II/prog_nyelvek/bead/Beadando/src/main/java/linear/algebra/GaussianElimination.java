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

    public GaussianElimination(String data) {
        this(data.split(" "));
    }

    public GaussianElimination(String[] data) {
        _rows = data.length;
        _cols = data[0].split(",").length;
        _matrix = new double[_rows][_cols];

        for (int i = 0; i < _rows; i++) {
            String rowStr = data[i];
            String[] numStr = rowStr.split(",");
            for (int j = 0; j < numStr.length; j++) {
                _matrix[i][j] = Double.parseDouble(numStr[j]);
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
        int h = 0, m = _rows;
        int k = 0, n = _cols;

        while (h < m && k < n){
            int iMax = argMax(h, k);
            if(_matrix[iMax][k] == 0){
                k++;
                h++;
            }
            else{
                swapRows(h, iMax);
                for (int i = h + 1; i < m; i++) {
//                    _matrix[i][k] = 0;
                    multiplyAndAddRow(h, i, k);
                }
                multiplyRow(h, k);
                h++;
                k++;
            }
        }
    }

    private int argMax(int h, int k){
        double [][] A = _matrix;
        int iMax = h;
        double max = Math.abs(A[h][k]);
        for(int i = h + 1; i < _rows; i++){
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
     * Adds a row by the scalar of another row
     * row2 = row2 + (row1 * scalar)
     * @param addRow int index of row to be added
     * @param mulRow int index or row that row1 is added to
     * @param colIndex double to scale row by
     *
     */
    private void multiplyAndAddRow(int addRow, int mulRow, int colIndex){
        for(int j = 0; j < _matrix[addRow].length; j++){
            double f = _matrix[addRow][colIndex] / _matrix[mulRow][colIndex];
            _matrix[addRow][j] -= f * mulRow;
        }
    }

    private void multiplyRow(int h, int k){
        double f = _matrix[h][k];
        for (int i = 0; i < _matrix[h].length; i++) {
            _matrix[h][i] /= f;
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
