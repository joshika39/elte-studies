package linear.algebra;


public class GaussianElimination {

    private static char[] variables = {'x', 'y', 'z', 'q', 'a', 'b', 'c'};
    private int rows;
    private int cols;
    private double[][] matrix;

    public int getCols() {
        return cols;
    }

    public int getRows() {
        return rows;
    }

    public double[][] getMatrix() {
        return matrix;
    }

    public void setMatrix(double[][] matrix) {
        checkMatrixDimensions(matrix);
        this.matrix = matrix;
    }

    public GaussianElimination(int rows, int cols, double[][] matrix) {
        this.rows = rows;
        this.cols = cols;
        this.matrix = matrix;
    }

    public GaussianElimination(String data) {
        this(stringsToDoubles(data.split(" ")));
    }

    public GaussianElimination(double[][] data) {
        rows = data.length;
        cols = data[0].length;
        matrix = data;
    }

    public void rowEchelonForm() {
        int h = 0;
        int k = 0;

        while (h < rows && k < cols) {
            int iMax = argMax(h, k);
            if (matrix[iMax][k] == 0) {
                k++;
            } else {
                swapRows(h, iMax);
                for (int i = h + 1; i < rows; i++) {
                    multiplyAndAddRow(i, h, k);
                }
                multiplyRow(h, k);
                h++;
                k++;
            }
        }
    }

    private int argMax(int h, int k) {
        int iMax = h;
        double max = Math.abs(matrix[h][k]);
        for (int i = h + 1; i < rows; i++) {
            if (Math.abs(matrix[i][k]) >= max) {
                iMax = i;
                max = Math.abs(matrix[i][k]);
            }
        }
        return iMax;
    }

    public void backSubstitution() {
        for (int i = rows - 1; i >= 0; i--) {
            if(matrix[i][i] == 0){
                throw new IllegalArgumentException();
            }
            for (int j = i - 1; j >= 0; j--) {
                multiplyAndAddRow(j, i, i);
            }
        }
    }

    /**
     * Swap positions of 2 rows
     *
     * @param rowIndex1 int index of row to swap
     * @param rowIndex2 int index of row to swap
     */
    private void swapRows(int rowIndex1, int rowIndex2) {
        // number of columns in matrix
        int numColumns = matrix[0].length;

        // holds number to be swapped
        double hold;

        for (int k = 0; k < numColumns; k++) {
            hold = matrix[rowIndex2][k];
            matrix[rowIndex2][k] = matrix[rowIndex1][k];
            matrix[rowIndex1][k] = hold;
        }
    }

    /**
     * Adds a row by the scalar of another row
     * row2 = row2 + (row1 * scalar)
     *
     * @param addRow   int index of row to be added
     * @param mulRow   int index or row that row1 is added to
     * @param colIndex double to scale row by
     */
    private void multiplyAndAddRow(int addRow, int mulRow, int colIndex) {
        double f = matrix[addRow][colIndex] / matrix[mulRow][colIndex];
        for (int j = 0; j < matrix[addRow].length; j++) {
            matrix[addRow][j] -= (f * matrix[mulRow][j]);
        }
    }

    private void multiplyRow(int h, int k) {
        double f = matrix[h][k];
        for (int i = 0; i < matrix[h].length; i++) {
            matrix[h][i] /= f;
        }
    }

    public void print() {
        print(matrix);
    }

    private void checkMatrixDimensions(double[][] matrix){
        if (matrix.length != rows || matrix[0].length != cols) {
            throw new IllegalArgumentException();
        }
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

    public static double[][] stringsToDoubles(String[] strings) {
        int colLen = strings[0].split(",").length;
        double[][] result = new double[strings.length][colLen]; 
        for (int i = 0; i < strings.length; i++) {
            String rowStr = strings[i];
            String[] numStr = rowStr.split(",");
            for (int j = 0; j < numStr.length; j++) {
                result[i][j] = Double.parseDouble(numStr[j]);
            }
        }
        return result;
    }
}
